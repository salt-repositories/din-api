using Din.Domain.Authorization.Requests;
using Din.Domain.Clients.Radarr.Requests;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Mediatr.Interfaces;
using MediatR;

namespace Din.Domain.Commands.Movies
{
    public class AddMovieCommand : IActivatedRequest, ITransactionRequest, IRequest<Movie>
    {
        public RadarrMovieRequest Movie { get; }

        public AddMovieCommand(RadarrMovieRequest movie)
        {
            Movie = movie;
        }
    }
}
