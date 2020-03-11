using System.Collections.Generic;
using Din.Domain.Clients.Abstractions;
using Din.Domain.Models.Querying;

namespace Din.Domain.Stores.Interfaces
{
    public interface IContentStore<T> where T : Content
    {
        ICollection<T> Content { get; }
        bool ShouldUpdate();
        (ICollection<T> collection, int count) GetAll(QueryParameters<T> queryParameters, Filters filters);
        T GetOneById(int id);
        void Set(ICollection<T> contentCollection);
        void AddOne(T content);
        void UpdateMultiple(ICollection<T> collection);
    }
}