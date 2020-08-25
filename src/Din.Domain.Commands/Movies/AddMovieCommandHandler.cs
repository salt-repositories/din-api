using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Din.Domain.BackgroundProcessing.BackgroundQueues.Concrete;
using Din.Domain.Clients.Radarr.Interfaces;
using Din.Domain.Context;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using MediatR;

namespace Din.Domain.Commands.Movies
{
    public class AddMovieCommandHandler : IRequestHandler<AddMovieCommand, Movie>
    {
        private readonly IRequestContext _context;
        private readonly IRadarrClient _client;
        private readonly IAccountRepository _accountRepository;
        private readonly IAddedContentRepository _addedContentRepository;
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;
        private readonly ContentPollingQueue _contentPollingQueue;

        public AddMovieCommandHandler
        (
            IRequestContext context,
            IRadarrClient client,
            IAccountRepository accountRepository,
            IAddedContentRepository addedContentRepository,
            IMovieRepository movieRepository,
            IMapper mapper,
            ContentPollingQueue contentPollingQueue
        )
        {
            _context = context;
            _client = client;
            _accountRepository = accountRepository;
            _addedContentRepository = addedContentRepository;
            _movieRepository = movieRepository;
            _mapper = mapper;
            _contentPollingQueue = contentPollingQueue;
        }

        public async Task<Movie> Handle(AddMovieCommand request, CancellationToken cancellationToken)
        {
            var response = await _client.AddMovieAsync(request.Movie, cancellationToken);
            
            Thread.Sleep(500);
            
            response = await _client.GetMovieByIdAsync(response.SystemId, cancellationToken);

            var movie = _movieRepository.Insert(_mapper.Map<Movie>(response));

            _contentPollingQueue.Enqueue(movie);

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