using MiniBank.BusinessLogic.Entities;

namespace MiniBank.BusinessLogic.Ports
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetByAccountIdAsync(int accountId);
        Task<Transaction> CreateTransactionAsync(Transaction transaction);
    }
}
