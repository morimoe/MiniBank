using Microsoft.AspNetCore.Mvc;
using MiniBank.BusinessLogic.DTO;
using MiniBank.BusinessLogic.Services;

namespace MiniBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDto>> GetById(int id)
        {
            var account = await _accountService.GetByIdAsync(id);
            return Ok(account);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDto>>> GetByUser([FromQuery] int userId)
        {
            var accounts = await _accountService.GetByUserIdAsync(userId);
            return Ok(accounts);
        }
    }
}
