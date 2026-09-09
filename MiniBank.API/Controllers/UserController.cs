using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniBank.BusinessLogic.DTO;
using MiniBank.BusinessLogic.Services;
using System.Security.Claims;

namespace MiniBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<UserDto?>> GetByIdAsync()
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var account = await _userService.GetByIdAsync(currentUserId);
            if (account == null) { return NotFound(); }
            return account;
        }
    }
}
