using System;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Radarr.Interfaces;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Context;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using MediatR;

namespace Din.Domain.Commands.Movies
{
    public class AddMovieCommandHandler : IRequestHandler<AddMovieCommand, RadarrMovie>
    {
        private readonly IRequestContext _context;
        private readonly IRadarrClient _client;
        private readonly IAccountRepository _accountRepository;
        private readonly IAddedContentRepository _addedContentRepository;

        public AddMovieCommandHandler(IRequestContext context, IRadarrClient client, IAccountRepository accountRepository,
            IAddedContentRepository addedContentRepository)
        {
            _context = context;
            _client = client;
            _accountRepository = accountRepository;
            _addedContentRepository = addedContentRepository;
        }

        public async Task<RadarrMovie> Handle(AddMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _client.AddMovieAsync(request.Movie, cancellationToken);

            var addedContentEntity = new AddedContent
            {
                ForeignId = movie.TmdbId,
                SystemId = movie.SystemId,
                Title = movie.Title,
                DateAdded = DateTime.Now,
                Status = ContentStatus.Queued,
                Account = await _accountRepository.GetAccountById(_context.GetIdentity(), cancellationToken),
                Type = ContentType.Movie
            };

            _addedContentRepository.Insert(addedContentEntity);

            return movie;
        }
    }
}
