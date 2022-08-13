using System;
using Din.Domain.Authorization.Requests;
using Din.Infrastructure.DataAccess.Mediatr.Interfaces;
using MediatR;

namespace Din.Domain.Commands.Accounts
{
    public class DeleteAccountCommand : IAuthorizedIdentityRequest, IActivatedRequest, ITransactionRequest<Unit>
    {
        public Guid Identity { get; }

        public DeleteAccountCommand(Guid id)
        {
            Identity = id;
        }
    }
}