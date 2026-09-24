using MiniBank.BusinessLogic.DTO;
using static MiniBank.BusinessLogic.Services.TransactionService;

namespace MiniBank.BusinessLogic.Services
{
    public interface ITransactionService
    {
        Task<TransferResultDto> TransferAsync(int currentUserId, string fromAccountNumber, string toAccountNumber, decimal amount, string description);
        Task<(TransactionHistoryResult Result, IEnumerable<TransactionDto>? Data)> TransactionHistoryAsync(string accountNumber, int currentUserId);
    }
}
