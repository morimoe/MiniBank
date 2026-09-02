using MiniBank.BusinessLogic.DTO;
using MiniBank.BusinessLogic.Ports;

namespace MiniBank.BusinessLogic.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<List<AccountDto>> GetUserAccountsAsync(int userId)
        {
            var accounts = await _accountRepository.GetByUserIdAsync(userId);
            return accounts.Select(a => new AccountDto
            {
                Id = a.Id,
                AccountNumber = a.AccountNumber,
                Currency = a.Currency ?? "",
                Balance = a.Balance
            }).ToList();
        }

        public async Task<AccountDto?> GetByIdAsync(int id)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if (account is null) return null;

            return new AccountDto
            {
                Id = account.Id,
                AccountNumber = account.AccountNumber,
                Currency = account.Currency ?? "",
                Balance = account.Balance
            };
        }
    }
}