using MiniBank.BusinessLogic.DTO;
using MiniBank.BusinessLogic.Entities;

namespace MiniBank.BusinessLogic.Services
{
    public interface IAuthService
    {
        Task<LoginResultDto> LoginAsync(string username, string password);
    }
}
