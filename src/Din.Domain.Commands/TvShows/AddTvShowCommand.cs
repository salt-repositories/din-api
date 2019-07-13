using System;
using Din.Domain.Authorization.Requests;
using Din.Domain.Clients.Sonarr.Requests;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Mediatr.Interfaces;
using Din.Domain.Stores.Interfaces;
using Din.Infrastructure.DataAccess.Mediatr.Interfaces;
using MediatR;

namespace Din.Domain.Commands.TvShows
{
    public class AddTvShowCommand : IContentAdditionRequest, IActivatedRequest, ITransactionRequest, IRequest<SonarrTvShow>
    {
        public SonarrTvShowRequest TvShow { get; }

        public AddTvShowCommand(SonarrTvShowRequest tvShow)
        {
            TvShow = tvShow;
        }
    }
}
