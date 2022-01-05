using System;

namespace Din.Domain.Models.Entities
{
    public interface IEntity
    {
        public Guid Id { get; set; }
    }
}