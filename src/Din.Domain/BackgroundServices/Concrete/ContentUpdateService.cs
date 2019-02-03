using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Din.Domain.BackgroundServices.Abstractions;
using Din.Domain.Clients.Interfaces;
using Din.Domain.Clients.ResponseObjects;
using Din.Infrastructure.DataAccess;
using Din.Infrastructure.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Din.Domain.BackgroundServices.Concrete
{
    public class ContentUpdateService : ScheduledProcessor
    {
        protected override string Schedule => "*/1 * * * *";

        public ContentUpdateService(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }

        public override async Task ProcessInScope(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<DinContext>();
            var movieClient = serviceProvider.GetService<IMovieClient>();
            var tvShowClient = serviceProvider.GetService<ITvShowClient>();

            await UpdateStatusMovieObjects(context, movieClient);
            await UpdateStatusTvShowObjects(context, tvShowClient);

            await context.SaveChangesAsync();
        }

        private async Task UpdateStatusMovieObjects(DinContext context, IMovieClient movieClient)
        {
            var content = await context.AddedContent.Where(c =>
                c.Type.Equals(ContentType.Movie) && !c.Status.Equals(ContentStatus.Done)).ToListAsync();
            var queue = (await movieClient.GetQueue<McQueueItem>()).ToList();
            var now = DateTime.Now;


            foreach (var c in content)
            {
                var movie = await movieClient.GetMovieByIdAsync(c.SystemId);
                var item = queue.Find(q => q.Movie.SystemId.Equals(c.SystemId));


                if (movie.Downloaded)
                {
                    c.Status = ContentStatus.Done;
                    continue;
                }

                if (item != null)
                {
                    c.Status = ContentStatus.Downloading;
                    c.Percentage = Math.Round(1 - (item.SizeLeft / item.Size), 2);
                    c.Eta = (int) item.TimeLeft.TotalSeconds;
                }

                if (now >= c.DateAdded.AddDays(2) && c.Percentage > 0.0)
                {
                    c.Status = ContentStatus.Stuck;
                    continue;
                }

                if (now >= c.DateAdded.AddDays(3))
                {
                    c.Status = ContentStatus.NotAvailable;
                }
            }
        }

        private async Task UpdateStatusTvShowObjects(DinContext context, ITvShowClient tvShowClient)
        {
            var content = await context.AddedContent.Where(c => c.Type.Equals(ContentType.TvShow) && !c.Status.Equals(ContentStatus.Done)).ToListAsync();
            var now = DateTime.Now;

            foreach (var c in content)
            {
                var show = await tvShowClient.GetTvShowByIdAsync(c.SystemId);
                show.Seasons.Remove(show.Seasons.First(s => s.SeasonsNumber.Equals(0)));

                var seasonsDone = 0;
                var seasonPercentage = new List<double>();

                foreach (var s in show.Seasons)
                {
                    if (s.Statistics.EpisodeCount.Equals(s.Statistics.TotalEpisodeCount))
                    {
                        seasonsDone++;
                        continue;
                    }

                    seasonPercentage.Add(Convert.ToDouble(s.Statistics.EpisodeCount) /
                                         (Convert.ToDouble(s.Statistics.TotalEpisodeCount) / 100));
                }

                if (seasonsDone.Equals(show.Seasons.Count))
                {
                    c.Status = ContentStatus.Done;
                    continue;
                }

                var showPercentage = Math.Round((seasonPercentage.Sum() / seasonPercentage.Count) / 100, 2);

                if (showPercentage > 0.0)
                {
                    c.Status = ContentStatus.Downloading;
                    c.Percentage = showPercentage;
                }

                if (now >= c.DateAdded.AddDays(2) && showPercentage > 0.0)
                {
                    c.Status = ContentStatus.Stuck;
                    c.Percentage = showPercentage;
                    continue;
                }

                if (now >= c.DateAdded.AddDays(5))
                {
                    c.Status = ContentStatus.NotAvailable;
                }
            }
        }
    }
}