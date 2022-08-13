using System;
using Din.Domain.Authorization.Requests;
using Din.Infrastructure.DataAccess.Mediatr.Interfaces;
using MediatR;

namespace Din.Domain.Commands.Accounts
{
    public class ActivateAccountCommand : IAuthorizedIdentityRequest, ITransactionRequest<Unit>
    {
        public Guid Identity { get; }
        public string Code { get; }

        public ActivateAccountCommand(Guid id, string code)
        {
            Identity = id;
            Code = code;
        }

    }
}
