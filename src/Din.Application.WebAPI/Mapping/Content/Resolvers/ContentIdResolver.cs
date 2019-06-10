using AutoMapper;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Clients.Sonarr.Responses;

namespace Din.Application.WebAPI.Mapping.Content.Resolvers
{
    public class ContentIdResolver<TSource, TDestination> : IValueResolver<TSource, TDestination, int> where TSource : Domain.Clients.Abstractions.Content
    {
        public int Resolve(TSource source, TDestination destination, int destMember, ResolutionContext context)
        {
            if (source.GetType() == typeof(RadarrMovie))
            {
                return source.SystemId;
            }

            if (source.GetType() == typeof(SonarrTvShow))
            {
                return source.SystemId;
            }

            return -8008;
        }
    }
}