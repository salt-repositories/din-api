using System;
using System.Collections.Generic;
using System.Linq;
using Din.Domain.Clients.Abstractions;
using Din.Domain.Extensions;
using Din.Domain.Stores.Interfaces;

namespace Din.Domain.Stores.Concrete
{
    public class ContentStore<T> : IContentStore<T> where T : Content 
    {
        public ICollection<T> Content { get; private set; }
        private DateTime _storeDare;

        public bool ShouldUpdate()
        {
            return Content == null || _storeDare.AddHours(1) <= DateTime.Now;
        }

        public ICollection<T> GetAll()
        {
            return Content;
        }

        public T GetOneById(int id)
        {
            return Content?.FirstOrDefault(c => c.SystemId.Equals(id));
        }

        public ICollection<T> GetMultipleByTitle(string title)
        {
            return Content.Where(movie => title.CalculateSimilarity(movie.Title) > 0.4).ToList();
        }

        public void Set(ICollection<T> contentCollection)
        {
            _storeDare = DateTime.Now;
            Content = contentCollection;
        }

        public void AddOne(T content)
        {
            Content?.Add(content);
        }
    }
}
