using Din.Service.Dto.Jwt;

namespace Din.Service.Services.Interfaces
{
    public interface ITokenService
    {
        (bool valid, string token) CreateToken(TokenRequestDto data);
    }
}
