using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Din.Data.Entities;
using Din.Service.Dto.Context;
using Din.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;


namespace Din.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region injections

        private readonly IAccountService _service;
        private readonly IMapper _mapper;
      

        #endregion injections

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
        [Authorize, HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AccountDto>), 200)]
        public async Task<IActionResult> GetAccounts()
        {
            return Ok(_mapper.Map<IEnumerable<AccountDto>>(await _service.GetAccountsAsync()));
        }

        /// <summary>
        /// Get account by ID
        /// </summary>
        /// <param name="id">Account ID</param>
        /// <returns>Single account</returns>
        [Authorize, HttpGet("{id}")]
        [ProducesResponseType(typeof(AccountDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAccountById([FromRoute] int id)
        {
            return Ok(_mapper.Map<AccountDto>(await _service.GetAccountByIdAsync(id)));
        }

        /// <summary>
        /// Create account
        /// </summary>
        /// <param name="account">Account model</param>
        /// <returns>Created account</returns>
        [Authorize, HttpPost]
        [ProducesResponseType(typeof(AccountDto), 201)]
        public async Task<IActionResult> CreateAccount([FromBody] AccountDto account)
        {
            return Created("Account created:", _mapper.Map<AccountDto>(await _service.CreateAccountAsync(_mapper.Map<AccountEntity>(account))));
        }

        /// <summary>
        /// Update existing account
        /// </summary>
        /// <param name="id">Account ID</param>
        /// <param name="data">updated data/ properties</param>
        /// <returns>Updated Account</returns>
        [Authorize, HttpPatch("{id}")]
        [ProducesResponseType(typeof(AccountDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAccount([FromRoute] int id, [FromBody] JsonPatchDocument<AccountDto> data)
        {
            var entity = await _service.GetAccountByIdAsync(id);
            var update =_mapper.Map<JsonPatchDocument<AccountEntity>>(data);
            update.ApplyTo(entity);

            return Ok(_mapper.Map<AccountDto>(await _service.UpdateAccountAsync(entity)));
        }

        #endregion endpoints
    }
}