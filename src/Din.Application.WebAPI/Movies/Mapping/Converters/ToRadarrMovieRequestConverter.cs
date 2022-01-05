﻿using System.Collections.Generic;
using AutoMapper;
using Din.Application.WebAPI.Movies.Requests;
using Din.Domain.Clients.Abstractions;
using Din.Domain.Clients.Radarr.Requests;

namespace Din.Application.WebAPI.Movies.Mapping.Converters
{
    public class ToRadarrMovieRequestConverter : ITypeConverter<MovieRequest, RadarrMovieRequest>
    {
        public RadarrMovieRequest Convert(MovieRequest source, RadarrMovieRequest destination, ResolutionContext context)
        {
            return new RadarrMovieRequest
            {
                TmdbId = source.TmdbId,
                Title = source.Title,
                Year = source.Year,
                TitleSlug = GenerateTitleSlug(source.Title, source.Year),
                QualityProfileId = 6,
                Monitored = true,
                Images = new List<ContentImage>
                {
                    new ContentImage
                    {
                        CoverType = "poster",
                        Url = source.PosterPath
                    }
                },
                MovieOptions = new RadarrRequestOptions
                {
                    SearchForMovie = true
                }
            };
        }

        private string GenerateTitleSlug(string title, int year)
        {
            return $"{title.ToLower().Replace(" ", "-")}-{year}";
        }     
    }
}
