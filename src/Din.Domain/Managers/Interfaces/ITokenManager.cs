using System;
using Din.Domain.Models.Entities;

namespace Din.Domain.Managers.Interfaces
{
    public interface ITokenManager
    {
        string GenerateJWtToken(Guid id, AccountRole role);
        string GenerateRefreshToken(Guid id, DateTime creationDate);
    }
}
