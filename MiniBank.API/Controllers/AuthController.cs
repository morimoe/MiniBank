using Microsoft.AspNetCore.Mvc;
using MiniBank.BusinessLogic.DTO;
using MiniBank.BusinessLogic.Ports;
using MiniBank.BusinessLogic.Services;

namespace MiniBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto data)
        {
            var token = await _authService.LoginAsync(data.Email, data.Password);
            if (token.Success == false) { return BadRequest(token); }
            return Ok(token);
        }
    }
}
