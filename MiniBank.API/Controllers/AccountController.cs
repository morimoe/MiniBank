using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniBank.BusinessLogic.DTO;
using MiniBank.BusinessLogic.Services;
using System.Security.Claims;

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
        
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDto>>> GetByUser()
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var accounts = await _accountService.GetByUserIdAsync(currentUserId);
            return Ok(accounts);
        }
        
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDto>> GetById(int id)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var account = await _accountService.GetByIdAsync(id, currentUserId);
            if (account is null) return NotFound();
            return Ok(account);
        }
    }
}
