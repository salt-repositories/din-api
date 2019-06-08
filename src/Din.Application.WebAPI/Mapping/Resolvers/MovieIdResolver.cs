using AutoMapper;
using Din.Application.WebAPI.Models.Response;
using Din.Domain.Clients.Radarr.Responses;

namespace Din.Application.WebAPI.Mapping.Resolvers
{
    public class MovieIdResolver : IValueResolver<RadarrMovie, MovieResponse, int>
    {
        public int Resolve(RadarrMovie source, MovieResponse destination, int destMember, ResolutionContext context)
        {
            return source.SystemId;
        }
    }
}
