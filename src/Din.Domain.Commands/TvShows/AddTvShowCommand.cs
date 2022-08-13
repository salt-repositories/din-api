using Din.Domain.Authorization.Requests;
using Din.Domain.Clients.Sonarr.Requests;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Mediatr.Interfaces;

namespace Din.Domain.Commands.TvShows
{
    public class AddTvShowCommand : IActivatedRequest, ITransactionRequest<TvShow>
    {
        public SonarrTvShowRequest TvShow { get; }

        public AddTvShowCommand(SonarrTvShowRequest tvShow)
        {
            TvShow = tvShow;
        }
    }
}
