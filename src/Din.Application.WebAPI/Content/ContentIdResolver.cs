using AutoMapper;

namespace Din.Application.WebAPI.Content
{
    public class ContentIdResolver<TSource, TDestination> : IValueResolver<TSource, TDestination, int> where TSource : Domain.Clients.Abstractions.Content
    {
        public int Resolve(TSource source, TDestination destination, int destMember, ResolutionContext context)
        {
            return source.SystemId;
        }
    }
}