using Din.Domain.Models.Dtos;

namespace Din.Domain.Stores.Interfaces
{
    public interface IRefreshTokenStore
    {
        void Insert(RefreshTokenDto refreshToken);
        RefreshTokenDto Get(string token);
        void Invoke(RefreshTokenDto refreshToken);
    }
}
