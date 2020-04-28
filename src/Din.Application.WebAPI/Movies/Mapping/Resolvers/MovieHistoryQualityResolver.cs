using AutoMapper;
using Din.Application.WebAPI.Movies.Responses;
using Din.Domain.Clients.Radarr.Responses;

namespace Din.Application.WebAPI.Movies.Mapping.Resolvers
{
    public class MovieHistoryQualityResolver : IValueResolver<RadarrHistoryRecord, MovieHistoryResponse, QualityResponse>
    {
        public QualityResponse Resolve(RadarrHistoryRecord source, MovieHistoryResponse destination, QualityResponse destMember,
            ResolutionContext context)
        {
            return new QualityResponse
            {
                Id = source.Quality.Quality.Id,
                Name = source.Quality.Quality.Name,
                Modifier = source.Quality.Quality.Modifier,
                Resolution = source.Quality.Quality.Resolution,
                Source = source.Quality.Quality.Source,
            };
        }
    }
}
