using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Din.Application.WebAPI.Models.RequestsModels;
using Din.Application.WebAPI.Models.ViewModels;
using Din.Application.WebAPI.Querying;
using Din.Application.WebAPI.Versioning;
using Din.Domain.Models.Entities;
using Din.Domain.Models.Querying;
using Din.Domain.Queries.Accounts;
using Din.Domain.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using static Din.Application.WebAPI.Versioning.ApiVersions;

namespace Din.Application.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion(V1)]
    [VersionedRoute("accounts")]
    [ControllerName("Accounts")]
    [Produces("application/json")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        #region fields

        private readonly IMediator _bus;
        private readonly IAccountService _service;
        private readonly IMapper _mapper;


        #endregion fields

        #region constructors

        public AccountController(IMediator bus, IAccountService service, IMapper mapper)
        {
            _bus = bus;
            _service = service;
            _mapper = mapper;
        }

        #endregion constructors

        #region endpoints
        /// <summary>
        /// Get all accounts
        /// </summary>
        /// <returns>Collection containing all accounts</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AccountViewModel>), 200)]
        public async Task<IActionResult> GetAccounts([FromQuery] QueryParametersRequest queryParameters)
        {
            var query = new GetAccountsQuery(_mapper.Map<QueryParameters<Account>>(queryParameters));
            var result = await _bus.Send(query);

            return Ok(_mapper.Map<QueryResponse<AccountViewModel>>(result));
        }

        /// <summary>
        /// Get account by ID
        /// </summary>
        /// <param name="id">Account ID</param>
        /// <returns>Single account</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AccountViewModel), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAccountById([FromRoute] Guid id)
        {
            return Ok(_mapper.Map<AccountViewModel>(await _service.GetAccountByIdAsync(id)));
        }

        /// <summary>
        /// Get addedcontent from account by ID
        /// </summary>
        /// <param name="id">Account ID</param>
        /// <returns></returns>
        [HttpGet("{id}/added_content")]
        [ProducesResponseType(typeof(IEnumerable<AddedContentViewModel>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAccountAddedContent([FromRoute] Guid id)
        {
            return Ok(_mapper.Map<IEnumerable<AddedContentViewModel>>(await _service.GetAccountAddedContent(id)));
        }

        /// <summary>
        /// Create account
        /// </summary>
        /// <param name="account">Account model</param>
        /// <returns>Created account</returns>
        [HttpPost]
        [ProducesResponseType(typeof(AccountViewModel), 201)]
        public async Task<IActionResult> CreateAccount([FromBody] AccountRequest account)
        {
            return Created("Account created:", _mapper.Map<AccountViewModel>(await _service.CreateAccountAsync(new Account
            {
                Username =  account.Username,
                Hash = BCrypt.Net.BCrypt.HashPassword(account.Password),
                Role = account.Role
            })));
        }

        /// <summary>
        /// Update existing account
        /// </summary>
        /// <param name="id">Account ID</param>
        /// <param name="account">updated data/ properties</param>
        /// <returns>Updated Account</returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(AccountViewModel), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAccount([FromRoute] Guid id, [FromBody] JsonPatchDocument<AccountRequest> account)
        {
            var currentAccount = await _service.GetAccountByIdAsync(id);
            var update =_mapper.Map<JsonPatchDocument<Account>>(account);
            update.ApplyTo(currentAccount);

            return Ok(_mapper.Map<AccountViewModel>(await _service.UpdateAccountAsync(currentAccount)));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAccount([FromRoute] Guid id)
        {
            await _service.DeleteAccountById(id);
            return NoContent();
        }

        #endregion endpoints
    }
}