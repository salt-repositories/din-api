using System;
using Din.Domain.Models.Entities;

namespace Din.Domain.Helpers.Interfaces
{
    public interface ITokenHelper
    {
        string GenerateJWtToken(Guid id, AccountRole role);
        string GenerateRefreshToken(Guid id, DateTime creationDate);
    }
}
