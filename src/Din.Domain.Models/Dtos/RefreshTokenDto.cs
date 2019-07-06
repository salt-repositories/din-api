using System;

namespace Din.Domain.Models.Dtos
{
    public class RefreshTokenDto
    {
        public string Token { get; set; }
        public Guid AccountIdentity { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
