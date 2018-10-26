using System.Threading.Tasks;
using Din.Service.Dto.Context;
using Din.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;


namespace Din.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region injections

        private readonly IAccountService _service;

        #endregion injections

        #region constructors

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        #endregion constructors

        #region endpoints
        /// <summary>
        /// Get all accounts
        /// </summary>
        /// <returns>Collection containing all accounts</returns>
        [Authorize, HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            return Ok(await _service.GetAccountsAsync());
        }

        /// <summary>
        /// Get account by ID
        /// </summary>
        /// <param name="id">Account ID</param>
        /// <returns>Single account</returns>
        [Authorize, HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            return Ok(await _service.GetAccountByIdAsync(id));
        }

        /// <summary>
        /// Create a account
        /// </summary>
        /// <param name="account">Account model</param>
        /// <returns>Created account</returns>
        [Authorize, HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountDto account)
        {
            return Created("Account created:", await _service.CreateAccountAsync(account));
        }

        /// <summary>
        /// Update existing account
        /// </summary>
        /// <param name="id">Account ID</param>
        /// <param name="data">updated data/ properties</param>
        /// <returns>Updated Account</returns>
        [Authorize, HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody]JsonPatchDocument<AccountDto> data)
        {
            var account = await _service.GetAccountByIdAsync(id);
            data.ApplyTo(account);
            await _service.UpdateAccountAsync(account);

            return Ok(account);
        }

        #endregion endpoints
    }
}