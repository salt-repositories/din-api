using Din.Domain.Authorization.Requests;
using Din.Domain.Clients.Sonarr.Requests;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Mediatr.Interfaces;
using MediatR;

namespace Din.Domain.Commands.TvShows
{
    public class AddTvShowCommand : IActivatedRequest, ITransactionRequest, IRequest<TvShow>
    {
        public SonarrTvShowRequest TvShow { get; }

        public AddTvShowCommand(SonarrTvShowRequest tvShow)
        {
            TvShow = tvShow;
        }
    }
}
