using Microsoft.AspNetCore.Mvc;
using MiniBank.BusinessLogic.Services;

namespace MiniBank.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserAccounts([FromQuery] int userId)
        {
            var accounts = await _accountService.GetUserAccountsAsync(userId);
            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var account = await _accountService.GetByIdAsync(id);
            if (account is null) return NotFound();
            return Ok(account);
        }
    }
}