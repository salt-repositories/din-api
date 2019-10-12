using System.Collections.Generic;
using Din.Domain.Models.Querying;

namespace Din.Domain.Stores.Interfaces
{
    public interface IContentStore<T>
    {
        ICollection<T> Content { get; }
        bool ShouldUpdate();
        (ICollection<T> collection, int count) GetAll(QueryParameters<T> queryParameters, Filters filters);
        T GetOneById(int id);
        void Set(ICollection<T> contentCollection);
        void AddOne(T content); 
    }
}