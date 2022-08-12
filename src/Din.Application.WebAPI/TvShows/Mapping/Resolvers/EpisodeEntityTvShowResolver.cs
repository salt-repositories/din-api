using System.Threading;
using AutoMapper;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;

namespace Din.Application.WebAPI.TvShows.Mapping.Resolvers
{
    public class EpisodeEntityTvShowResolver : IValueResolver<SonarrEpisode, Episode, TvShow>
    {
        private readonly ITvShowRepository _repository;

        public EpisodeEntityTvShowResolver(ITvShowRepository repository)
        {
            _repository = repository;
        }

        public TvShow Resolve(SonarrEpisode source, Episode destination, TvShow destMember, ResolutionContext context)
        {
            return _repository.GetTvShowBySystemId(source.SeriesId, CancellationToken.None).GetAwaiter().GetResult();
        }
    }
}
