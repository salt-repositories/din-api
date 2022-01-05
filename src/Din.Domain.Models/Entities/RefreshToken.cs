using System;

namespace Din.Domain.Models.Entities
{
    public class RefreshToken : IEntity
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public Guid AccountIdentity { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
