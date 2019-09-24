using System.Collections.Generic;

namespace Din.Domain.Stores.Interfaces
{
    public interface IContentStore<T>
    {
        ICollection<T> Content { get; }

        bool ShouldUpdate();
        ICollection<T> GetAll();
        T GetOneById(int id);
        ICollection<T> GetMultipleByTitle(string title);
        void Set(ICollection<T> contentCollection);
        void AddOne(T content);
    }
}