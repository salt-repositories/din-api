﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Exceptions.Concrete;
using Din.Domain.Helpers;
using Din.Domain.Managers.Interfaces;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using MediatR;
using BC = BCrypt.Net.BCrypt;

namespace Din.Domain.Commands.Accounts
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Account>
    {
        private readonly IAccountRepository _repository;
        private readonly IEmailManager _emailManager;

        public CreateAccountCommandHandler(IAccountRepository repository, IEmailManager emailManager)
        {
            _repository = repository;
            _emailManager = emailManager;
        }

        public async Task<Account> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            if (await _repository.GetAccountByUsername(request.Account.Username, cancellationToken) != null)
            {
                throw new EntityCreationException("The username is already registered");
            }

            if (await _repository.GetAccountByEmail(request.Account.Email, cancellationToken) != null)
            {
                throw new EntityCreationException("The email is already registered");
            }

            request.Account.Hash = BC.HashPassword(request.Password);
            request.Account.Active = false;

            var authorizationCode = RandomCodeGenerator.GenerateRandomCode(30, false);

            request.Account.Codes = new List<AccountAuthorizationCode>
            {
                new AccountAuthorizationCode
                {
                    Account = request.Account,
                    Active = true,
                    Code = BC.HashPassword(authorizationCode),
                    Generated = DateTime.Now
                }
            };

            await _emailManager.SendInvitation(
                request.Account.Email,
                request.Account.Username,
                request.Account.Role.ToString(),
                authorizationCode,
                cancellationToken
            );

            _repository.Insert(request.Account);

            return request.Account;
        }
    }
}