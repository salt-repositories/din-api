using System;
using Din.Domain.Authorization.Requests;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Mediatr.Interfaces;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Din.Domain.Commands.Accounts
{
    public class UpdateAccountCommand : IAuthorizedIdentityRequest, IUpdateRequest<Account>, ITransactionRequest, IRequest<Account>
    {
        public Guid Identity { get; }
        public JsonPatchDocument<Account> Update { get; }

        public UpdateAccountCommand(Guid id, JsonPatchDocument<Account> update)
        {
            Identity = id;
            Update = update;
        }
    }
}
