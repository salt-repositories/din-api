using System;
using System.Collections.Generic;
using System.Linq;
using Din.Domain.Clients.Abstractions;
using Din.Domain.Extensions;
using Din.Domain.Models.Querying;
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

        public ICollection<T> GetAll(QueryParameters<T> queryParameters, Filters filters)
        {
            var collection = ApplyFilters(filters);
            collection = collection.ApplyQueryParameters(queryParameters);

            return collection.ToList();
        }

        public T GetOneById(int id)
        {
            return Content?.FirstOrDefault(c => c.SystemId.Equals(id));
        }

        public void Set(ICollection<T> contentCollection)
        {
            _storeDate = DateTime.Now;
            Content = contentCollection;
        }

        public void AddOne(T content)
        {
            Content?.ToList().Add(content);
        }

        public int Count(Filters filters)
        {
            var content = ApplyFilters(filters);

            return content.Count();
        }

        private IEnumerable<T> ApplyFilters(Filters filters)
        {
            var content = Content;

            foreach (var filter in filters.GetType().GetProperties())
            {
                try
                {
                    var value = filter.GetValue(filters, null).ToString();

                    if (filter.Name.ToLower().Equals("title") && !string.IsNullOrEmpty(value))
                    {
                        content = content
                            .Where(x => value.CalculateSimilarity(x.Title) > 0.4)
                            .Concat(content.Where(x => x.Title.ToLower().Contains(value.ToLower())))
                            .ToList();

                        continue;
                    }

                    content = content.ApplyFilter(filter.Name, value).ToList();
                }
                catch
                {
                    // ignored
                }
            }

            return content;
        }
    }
}