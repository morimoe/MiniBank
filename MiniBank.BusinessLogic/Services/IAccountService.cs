using MiniBank.BusinessLogic.DTO;

namespace MiniBank.BusinessLogic.Services
{
    public interface IAccountService
    {
        Task<AccountDto> GetByIdAsync(int id);
        Task<IEnumerable<AccountDto>> GetByUserIdAsync(int userId);
    }
}