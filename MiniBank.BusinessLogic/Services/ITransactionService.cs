using MiniBank.BusinessLogic.DTO;

namespace MiniBank.BusinessLogic.Services
{
    public interface ITransactionService
    {
        Task<TransferResultDto> TransferAsync(int currentUserId, string fromAccountNumber, string toAccountNumber, decimal amount, string description);
        Task<IEnumerable<TransactionDto>> TransactionHistoryAsync(string accountNumber, int currentUserId);
    }
}
