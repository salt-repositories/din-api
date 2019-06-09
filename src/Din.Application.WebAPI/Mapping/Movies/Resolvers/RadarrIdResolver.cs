using AutoMapper;
using Din.Domain.Clients.Radarr.Responses;

namespace Din.Application.WebAPI.Mapping.Movies.Resolvers
{
    public class RadarrIdResolver<TSource, TDestination> : IValueResolver<TSource, TDestination, int>
    {
        public int Resolve(TSource source, TDestination destination, int destMember, ResolutionContext context)
        {
            if (source.GetType() == typeof(RadarrMovie))
            {
                return (source as RadarrMovie).SystemId;
            }

            if (source.GetType() == typeof(RadarrCalendar))
            {
                return (source as RadarrCalendar).SystemId;
            }

            return 0;
        }
    }
}