using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniBank.BusinessLogic.DTO;
using MiniBank.BusinessLogic.Services;
using MiniBank.BusinessLogic.Settings;

namespace MiniBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly JwtSettings _jwtSettings;

        public AuthController(IAuthService authService, JwtSettings jwtSettings)
        {
            _authService = authService;
            _jwtSettings = jwtSettings;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto data)
        {
            var result = await _authService.LoginAsync(data.Identifier, data.Password);
            if (result.Success == false) { return BadRequest(result); }

            Response.Cookies.Append("token", result.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes),
                Path = "/"
            });

            return Ok(new { success = true });
        }

        [HttpGet("session")]
        public IActionResult Session()
        {
            return Ok(new { active = User.Identity?.IsAuthenticated == true });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("token");
            return Ok();
        }
    }
}