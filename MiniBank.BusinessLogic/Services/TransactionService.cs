using MiniBank.BusinessLogic.DTO;
using MiniBank.BusinessLogic.Ports;
using MiniBank.BusinessLogic.Entities;

namespace MiniBank.BusinessLogic.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;

        public TransactionService(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }
        public async Task<TransferResultDto> TransferAsync(int currentUserId, int fromAccountId, string toAccountNumber, decimal amount, string description)
        {
            var account = await _accountRepository.GetByIdAsync(fromAccountId);
            if (account == null ) { return TransferResultDto.Fail("Account doesn't exist"); }
            if (currentUserId != account.UserId) { return TransferResultDto.Fail("Access denied"); }

            var toAccount = await _accountRepository.GetByAccountNumberAsync(toAccountNumber);
            if (toAccount == null) { return TransferResultDto.Fail("Account doesn't exist"); }

            if (amount <= 0) { return TransferResultDto.Fail("Amount wasn't entered correctly"); }
            if (account.Balance < amount) { return TransferResultDto.Fail("Insufficient funds"); }

            if (fromAccountId == toAccount.Id) { return TransferResultDto.Fail("You can't transfer money to the same account"); }

            await _accountRepository.UpdateBalanceAsync(account.Id, account.Balance - amount);
            await _accountRepository.UpdateBalanceAsync(toAccount.Id, toAccount.Balance + amount);

            await _transactionRepository.CreateTransactionAsync(new Transaction { 
                FromAccountId = account.Id, 
                ToAccountId = toAccount.Id, 
                Amount = amount, 
                CreatedAt = DateTime.UtcNow, 
                Description = description, 
                Status = "Succeded"
            });

            return TransferResultDto.Pass();
        }

        public async Task<IEnumerable<TransactionDto>> TransactionHistoryAsync(int accountId, int currentUserId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null ) { return Enumerable.Empty<TransactionDto>(); }
            if (account.UserId != currentUserId) { return Enumerable.Empty<TransactionDto>(); }

            var transactions = await _transactionRepository.GetByAccountIdAsync(accountId);
            return transactions.Select(transaction => new TransactionDto
            {
                FromAccountId = transaction.FromAccountId,
                ToAccountId = transaction.ToAccountId,
                Amount = transaction.Amount,
                Description = transaction.Description,
                Status = transaction.Status,
                CreatedAt = transaction.CreatedAt
            });
        }
    }
}
