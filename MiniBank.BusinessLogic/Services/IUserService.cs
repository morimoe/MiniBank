using MiniBank.BusinessLogic.DTO;

namespace MiniBank.BusinessLogic.Services
{
    public interface IUserService
    {
        Task<UserDto?> GetByIdAsync(int id);
    }
}
