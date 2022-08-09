using System;
using Din.Domain.Models.Dtos;
using Din.Infrastructure.DataAccess.Mediatr.Interfaces;

namespace Din.Domain.Commands.Authentication
{
    public class RefreshTokenCommand : ITransactionRequest<TokenDto>
    {
        public string RefreshToken { get; }
        public DateTime RequestDate { get; }

        public RefreshTokenCommand(string refreshToken, DateTime requestDate)
        {
            RefreshToken = refreshToken;
            RequestDate = requestDate;
        }
    }
}