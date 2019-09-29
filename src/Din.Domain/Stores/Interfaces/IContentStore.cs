using System.Collections.Generic;
using Din.Domain.Models.Querying;

namespace Din.Domain.Stores.Interfaces
{
    public interface IContentStore<T>
    {
        ICollection<T> Content { get; }
        bool ShouldUpdate();
        ICollection<T> GetAll(QueryParameters<T> queryParameters, string title);
        T GetOneById(int id);
        void Set(ICollection<T> contentCollection);
        void AddOne(T content);
        int Count(string title);
    }
}