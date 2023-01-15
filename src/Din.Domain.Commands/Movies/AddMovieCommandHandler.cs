﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.BackgroundProcessing.BackgroundQueues.Concrete;
using Din.Domain.Clients.Radarr.Interfaces;
using Din.Domain.Context;
using Din.Domain.Mapping;
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
        private readonly IMovieRepository _movieRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly ContentPollingQueue _contentPollingQueue;

        public AddMovieCommandHandler
        (
            IRequestContext context,
            IRadarrClient client,
            IAccountRepository accountRepository,
            IAddedContentRepository addedContentRepository,
            IMovieRepository movieRepository,
            IGenreRepository genreRepository,
            ContentPollingQueue contentPollingQueue
        )
        {
            _context = context;
            _client = client;
            _accountRepository = accountRepository;
            _addedContentRepository = addedContentRepository;
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
            _contentPollingQueue = contentPollingQueue;
        }

        public async Task<Movie> Handle(AddMovieCommand request, CancellationToken cancellationToken)
        {
            var response = await _client.AddMovieAsync(request.Movie, cancellationToken);
            var movie = _movieRepository.Insert(response.ToEntity());
            
            var genres = new List<MovieGenre>();
            
            foreach (var genre in response.Genres)
            {
                genres.Add(new MovieGenre
                {
                    Genre = await _genreRepository.GetGenreByNameAsync(genre, cancellationToken),
                    Movie = movie
                });
            }

            movie.Genres = genres;

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