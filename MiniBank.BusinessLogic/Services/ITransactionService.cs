using MiniBank.BusinessLogic.DTO;

namespace MiniBank.BusinessLogic.Services
{
    public interface ITransactionService
    {
        Task<TransferResultDto> TransferAsync(int currentUserId, int fromAccountId, string toAccountNumber, decimal amount, string description);
        Task<IEnumerable<TransactionDto>> TransactionHistoryAsync(int accountId, int currentUserId);
    }
}
