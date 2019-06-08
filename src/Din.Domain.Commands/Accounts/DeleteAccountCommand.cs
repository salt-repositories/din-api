using System;
using Din.Domain.Authorization.Requests;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Mediatr.Interfaces;
using MediatR;

namespace Din.Domain.Commands.Accounts
{
    public class DeleteAccountCommand : IAuthorizedIdentityRequest, ITransactionRequest, IRequest
    {
        public Guid Identity { get; }

        public DeleteAccountCommand(Guid id)
        {
            Identity = id;
        }
    }
}