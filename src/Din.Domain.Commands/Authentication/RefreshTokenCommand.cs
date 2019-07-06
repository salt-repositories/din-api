using System;
using Din.Domain.Models.Dtos;
using MediatR;

namespace Din.Domain.Commands.Authentication
{
    public class RefreshTokenCommand : IRequest<TokenDto>
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