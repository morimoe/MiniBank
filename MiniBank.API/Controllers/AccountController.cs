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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDto>>> GetByUser([FromQuery] int userId)
        {
            var accounts = await _accountService.GetByUserIdAsync(userId);
            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDto>> GetById(int id)
        {
            // TODO: проверить, что account.UserId == текущий пользователь, после реализации авторизации
            // в принципе ещё добавить в других частях кода эту проверку
            var account = await _accountService.GetByIdAsync(id);
            if (account is null) return NotFound();
            return Ok(account);
        }
    }
}
