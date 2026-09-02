using MiniBank.BusinessLogic.Entities;

namespace MiniBank.BusinessLogic.Ports
{
    public interface IAccountRepository
    {
        Task<Account?> GetByIdAsync(int id);
        Task<List<Account>> GetByUserIdAsync(int userId);
        Task UpdateAsync(Account account);
    }
}