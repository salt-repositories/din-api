using System;
using Din.Domain.Models.Dtos;
using Din.Infrastructure.DataAccess.Mediatr.Interfaces;
using MediatR;

namespace Din.Domain.Commands.Authentication
{
    public class RefreshTokenCommand : ITransactionRequest, IRequest<TokenDto>
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