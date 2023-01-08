﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using Din.Application.WebAPI.Accounts.Requests;
using Din.Application.WebAPI.Accounts.Responses;
using Din.Application.WebAPI.Querying;
using Din.Application.WebAPI.Versioning;
using Din.Domain.Commands.Accounts;
using Din.Domain.Models.Entities;
using Din.Domain.Models.Querying;
using Din.Domain.Queries.Accounts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Din.Application.WebAPI.Accounts
{
    [ApiController]
    [ApiVersion(ApiVersions.V1)]
    [VersionedRoute("accounts")]
    [ControllerName("Accounts")]
    [Produces("application/json")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        #region fields

        private readonly IMediator _bus;
        private readonly IMapper _mapper;

        #endregion fields

        #region constructors

        public AccountController(IMediator bus, IMapper mapper)
        {
            _bus = bus;
            _mapper = mapper;
        }

        #endregion constructors

        #region endpoints

        /// <summary>
        /// Get accounts
        /// </summary>
        /// <param name="queryParameters">Optional query parameters</param>
        /// <returns>Collection containing all accounts</returns>
        [HttpGet]
        [ProducesResponseType(typeof(QueryResponse<AccountResponse>), 200)]
        public async Task<IActionResult> GetAccounts([FromQuery] QueryParametersRequest queryParameters)
        {
            var query = new GetAccountsQuery(_mapper.Map<QueryParameters>(queryParameters));
            var result = await _bus.Send(query);

            return Ok(_mapper.Map<QueryResponse<AccountResponse>>(result));
        }

        /// <summary>
        /// Get accounts V2
        /// </summary>
        /// <param name="queryParameters">Optional query parameters</param>
        /// <returns>Collection containing all accounts</returns>
        [HttpGet]
        [ApiVersion(ApiVersions.V2)]
        [ProducesResponseType(typeof(QueryResponse<AccountResponse>), 200)]
        public async Task<IActionResult> GetAccountsV2([FromQuery] QueryParametersRequest queryParameters)
        {
            var query = new GetAccountsQuery(_mapper.Map<QueryParameters>(queryParameters));
            var result = await _bus.Send(query);

            return Ok<QueryResponse<Account>>(result);
        }

        /// <summary>
        /// Get account by ID
        /// </summary>
        /// <param name="id">Account ID</param>
        /// <returns>Single account</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AccountResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAccountById([FromRoute] Guid id)
        {
            var query = new GetAccountQuery(id);
            return Ok(_mapper.Map<AccountResponse>(await _bus.Send(query)));
            
        }

        /// <summary>
        /// Get account by ID V2
        /// </summary>
        /// <param name="id">Account ID</param>
        /// <returns>Single account</returns>
        [HttpGet("{id}")]
        [ApiVersion(ApiVersions.V2)]
        [ProducesResponseType(typeof(AccountResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAccountByIdV2([FromRoute] Guid id)
        {
            var account = await _bus.Send(new GetAccountQuery(id));
            return Ok<AccountResponse>(account);
        }

        /// <summary>
        /// Activate account by code
        /// </summary>
        /// <param name="id">Account ID</param>
        /// <param name="code"></param>
        /// <returns>Single account</returns>
        [HttpGet("{id}/activate")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ActivateAccountByCode([FromRoute] Guid id, [FromQuery] string code)
        {
            var query = new ActivateAccountCommand(id, code);
            await _bus.Send(query);

            return NoContent();
        }

        /// <summary>
        /// Get added content from account by ID
        /// </summary>
        /// <param name="id">Account ID</param>
        /// <param name="queryParameters"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("{id}/added_content")]
        [ProducesResponseType(typeof(QueryResponse<AddedContentResponse>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAccountAddedContent
        (
            [FromRoute] Guid id,
            [FromQuery] QueryParametersRequest queryParameters,
            [FromQuery] AddedContentFilters filters
        )
        {
            var query = new GetAddedContentQuery(_mapper.Map<QueryParameters>(queryParameters), id, filters);
            var result = await _bus.Send(query);

            return Ok(_mapper.Map<QueryResponse<AddedContentResponse>>(result));
        }

        /// <summary>
        /// Create account
        /// </summary>
        /// <param name="account">Account request model</param>
        /// <returns>Created account</returns>
        [HttpPost]
        [ProducesResponseType(typeof(AccountResponse), 201)]
        public async Task<IActionResult> CreateAccount([FromBody] AccountRequest request)
        {
            var command = new CreateAccountCommand(request, request.Password);
            var result = await _bus.Send(command);

            return Created("", _mapper.Map<AccountResponse>(result));
        }

        /// <summary>
        /// Update existing account by ID
        /// </summary>
        /// <param name="id">Account ID</param>
        /// <param name="update">JSON patch document</param>
        /// <returns>Updated Account</returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(AccountResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAccount([FromRoute] Guid id,
            [FromBody] JsonPatchDocument<AccountRequest> update)
        {
            var command = new UpdateAccountCommand(id, _mapper.Map<JsonPatchDocument<Account>>(update));
            var result = await _bus.Send(command);

            return Ok(_mapper.Map<AccountResponse>(result));
        }

        /// <summary>
        /// Delete account by ID
        /// </summary>
        /// <param name="id">Account ID</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAccount([FromRoute] Guid id)
        {
            var command = new DeleteAccountCommand(id);
            await _bus.Send(command);

            return NoContent();
        }

        #endregion endpoints

        private static OkObjectResult Ok<T>([ActionResultObjectValue] T value) => new(value);
    }
}