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
        private DateTime _storeDate;

        public bool ShouldUpdate()
        {
            return Content == null || _storeDate.AddHours(1) <= DateTime.Now;
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
            return Content.Where(content => title.CalculateSimilarity(content.Title) > 0.4)
                .Concat(Content.Where(content => content.Title.ToLower().Contains(title.ToLower()))).ToList();
        }

        public void Set(ICollection<T> contentCollection)
        {
            _storeDate = DateTime.Now;
            Content = contentCollection;
        }

        public void AddOne(T content)
        {
            Content?.Add(content);
        }
    }
}
