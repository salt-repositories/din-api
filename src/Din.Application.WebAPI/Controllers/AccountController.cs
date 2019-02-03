﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Din.Application.WebAPI.Requests;
using Din.Application.WebAPI.ViewModels;
using Din.Domain.Services.Interfaces;
using Din.Infrastructure.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Din.Application.WebAPI.Controllers
{
    [ControllerName("Accounts")]
    [Authorize]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("v{version:apiVersion}/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region fields

        private readonly IAccountService _service;
        private readonly IMapper _mapper;


        #endregion fields

        #region constructors

        public AccountController(IAccountService service, IMapper mapper)
        {
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
        public async Task<IActionResult> GetAccounts()
        {
            return Ok(_mapper.Map<IEnumerable<AccountViewModel>>(await _service.GetAccountsAsync()));
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
            return Created("Account created:", _mapper.Map<AccountViewModel>(await _service.CreateAccountAsync(_mapper.Map<AccountEntity>(account))));
        }

        /// <summary>
        /// Update existing account
        /// </summary>
        /// <param name="id">Account ID</param>
        /// <param name="account">updated data/ properties</param>
        /// <returns>Updated Account</returns>
        [Authorize, HttpPatch("{id}")]
        [ProducesResponseType(typeof(AccountViewModel), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAccount([FromRoute] Guid id, [FromBody] JsonPatchDocument<AccountRequest> account)
        {
            var currentAccount = await _service.GetAccountByIdAsync(id);
            var update =_mapper.Map<JsonPatchDocument<AccountEntity>>(account);
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