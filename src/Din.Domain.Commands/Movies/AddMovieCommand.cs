using Din.Domain.Clients.Radarr.Requests;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Stores.Interfaces;
using Din.Infrastructure.DataAccess.Mediatr.Interfaces;
using MediatR;

namespace Din.Domain.Commands.Movies
{
    public class AddMovieCommand : IMediaAdditionRequest, ITransactionRequest, IRequest<RadarrMovie>
    {
        public RadarrMovieRequest Movie { get; }

        public AddMovieCommand(RadarrMovieRequest movie)
        {
            Movie = movie;
        }
    }
}
