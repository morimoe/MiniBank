using MiniBank.BusinessLogic.Entities;

namespace MiniBank.BusinessLogic.Ports
{
    public interface IAccountRepository
    {
        Task<Account> GetByIdAsync(int id);
        Task<IEnumerable<Account>> GetByUserIdAsync(int userId);
        Task UpdateBalanceAsync(int id, decimal balance);
    }
}