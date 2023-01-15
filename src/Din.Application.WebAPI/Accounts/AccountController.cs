using System;
using System.Linq;
using System.Threading.Tasks;
using Din.Application.WebAPI.Accounts.Requests;
using Din.Application.WebAPI.Accounts.Responses;
using Din.Application.WebAPI.Controller;
using Din.Application.WebAPI.Controller.Versioning;
using Din.Application.WebAPI.Querying;
using Din.Domain.Commands.Accounts;
using Din.Domain.Models.Entities;
using Din.Domain.Models.Querying;
using Din.Domain.Queries.Accounts;
using Din.Domain.Queries.Querying;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Din.Application.WebAPI.Accounts
{
    [ApiVersion(ApiVersions.V1)]
    [VersionedRoute("accounts")]
    [ControllerName("Accounts")]
    public class AccountController : ApiController
    {
        private readonly IMediator _bus;

        public AccountController(IMediator bus)
        {
            _bus = bus;
        }

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
            var result = await _bus.Send(new GetAccountsQuery(queryParameters));
            var response = ToAccountResponse(result);

            return Ok(response);
        }

        /// <summary>
        /// Get account by ID
        /// </summary>
        /// <param name="id">Account ID</param>
        /// <returns>Single account</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(AccountResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAccountById([FromRoute] Guid id)
        {
            var result = await _bus.Send(new GetAccountQuery(id));
            return Ok<AccountResponse>(result);
        }

        /// <summary>
        /// Activate account by code
        /// </summary>
        /// <param name="id">Account ID</param>
        /// <param name="code"></param>
        /// <returns>Single account</returns>
        [HttpGet("{id:guid}/activate")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ActivateAccountByCode([FromRoute] Guid id, [FromQuery] string code)
        {
            await _bus.Send(new ActivateAccountCommand(id, code));
            return NoContent();
        }

        /// <summary>
        /// Get added content from account by ID
        /// </summary>
        /// <param name="id">Account ID</param>
        /// <param name="queryParameters"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}/added_content")]
        [ProducesResponseType(typeof(QueryResponse<AddedContentResponse>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAccountAddedContent
        (
            [FromRoute] Guid id,
            [FromQuery] QueryParametersRequest queryParameters,
            [FromQuery] AddedContentFilters filters
        )
        {
            var query = new GetAddedContentQuery(queryParameters, id, filters);
            var result = await _bus.Send(query);
            var response = ToAddedContentResponse(result);

            return Ok(response);
        }

        /// <summary>
        /// Create account
        /// </summary>
        /// <param name="request">Account request model</param>
        /// <returns>Created account</returns>
        [HttpPost]
        [ProducesResponseType(typeof(AccountResponse), 201)]
        public async Task<IActionResult> CreateAccount([FromBody] AccountRequest request)
        {
            var result = await _bus.Send(new CreateAccountCommand(request, request.Password));
            return Created<AccountResponse>(result);
        }

        /// <summary>
        /// Update existing account by ID
        /// </summary>
        /// <param name="id">Account ID</param>
        /// <param name="update">JSON patch document</param>
        /// <returns>Updated Account</returns>
        [HttpPatch("{id:guid}")]
        [ProducesResponseType(typeof(AccountResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAccount([FromRoute] Guid id,
            [FromBody] JsonPatchDocument<AccountRequest> update)
        {
            var command = new UpdateAccountCommand(id, ConvertPatchDocument<AccountRequest, Account>(update));
            var result = await _bus.Send(command);

            return Ok<AccountResponse>(result);
        }

        /// <summary>
        /// Delete account by ID
        /// </summary>
        /// <param name="id">Account ID</param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAccount([FromRoute] Guid id)
        {
            await _bus.Send(new DeleteAccountCommand(id));
            return NoContent();
        }

        #endregion endpoints

        private static QueryResponse<AccountResponse> ToAccountResponse(QueryResult<Account> result) =>
            new(result.Items.Select(item => (AccountResponse) item), result.TotalCount);
        
        private static QueryResponse<AddedContentResponse> ToAddedContentResponse(QueryResult<AddedContent> result) =>
            new(result.Items.Select(item => (AddedContentResponse) item), result.TotalCount);
    }
}