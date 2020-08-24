using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;

namespace Din.Application.WebAPI.TvShows.Mapping.Resolvers
{
    public class TvShowEntityGenreResolver : IValueResolver<SonarrTvShow, TvShow, ICollection<TvShowGenre>>
    {
        private readonly DinContext _context;
        private readonly IGenreRepository _repository;

        public TvShowEntityGenreResolver(DinContext context, IGenreRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        public ICollection<TvShowGenre> Resolve(SonarrTvShow source, TvShow destination, ICollection<TvShowGenre> destMember,
            ResolutionContext context)
        {
            List<TvShowGenre> genres;

            if (source.Genres == null || source.Genres.Count() == destination.Genres.Count())
            {
                return destination.Genres;
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                genres = source.Genres.Select(genre => new TvShowGenre {Genre = _repository.GetGenreByName(genre) ?? _repository.Insert(new Genre {Name = genre}), TvShow = destination}).ToList();
                _context.SaveChanges();
                transaction.Commit();
            }

            return genres;
        }
    }
}