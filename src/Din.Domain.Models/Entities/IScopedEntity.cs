using System;

namespace Din.Domain.Models.Entities
{
    public interface IScopedEntity : IEntity
    {
        Guid AccountId { get; set; }
        Account Account { get; set; }
    }
}