using MiniBank.BusinessLogic.DTO;
using MiniBank.BusinessLogic.Ports;

namespace MiniBank.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<AccountDto?> GetByIdAsync(int id, int currentUserId)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if (account == null) { return null; }
            if (account.UserId != currentUserId)
            {
                return null;
            }
                        
            return new AccountDto
            {
                AccountNumber = account.AccountNumber,
                Currency = account.Currency,
                Balance = account.Balance
            };
        }
        public async Task<IEnumerable<AccountDto>> GetByUserIdAsync(int userId)
        {
            var accounts = await _accountRepository.GetByUserIdAsync(userId);
            return accounts.Select(account => new AccountDto
            {
                AccountNumber = account.AccountNumber,
                Currency = account.Currency,
                Balance = account.Balance
            });
        }
    }
}
