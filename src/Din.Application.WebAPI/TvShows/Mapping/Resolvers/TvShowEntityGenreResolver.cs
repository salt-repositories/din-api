using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;

namespace Din.Application.WebAPI.TvShows.Mapping.Resolvers
{
    public class TvShowEntityGenreResolver : IValueResolver<SonarrTvShow, TvShow, ICollection<TvShowGenre>>
    {
        private readonly IGenreRepository _repository;

        public TvShowEntityGenreResolver(IGenreRepository repository)
        {
            _repository = repository;
        }

        public ICollection<TvShowGenre> Resolve(SonarrTvShow source, TvShow destination, ICollection<TvShowGenre> destMember,
            ResolutionContext context)
        {
            List<TvShowGenre> genres = new List<TvShowGenre>();

            if (source.Genres == null)
            {
                return genres;
            }

            if (destination.Genres != null && destination.Genres.Count == source.Genres.Count())
            {
                return destination.Genres;
            }

            return source.Genres.Select(genre => new TvShowGenre {Genre = _repository.GetGenreByName(genre) ?? _repository.Insert(new Genre {Name = genre}), TvShow = destination}).ToList();
        }
    }
}