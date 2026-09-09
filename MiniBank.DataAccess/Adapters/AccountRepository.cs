using Microsoft.EntityFrameworkCore;
using MiniBank.BusinessLogic.Entities;
using MiniBank.BusinessLogic.Ports;
using MiniBank.DataAccess.Context;

namespace MiniBank.DataAccess.Adapters
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MiniBankDbContext _context;
        public AccountRepository(MiniBankDbContext context)
        {
            _context = context;
        }
        public async Task<Account?> GetByIdAsync(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
                return account;
        }

        public async Task<IEnumerable<Account>> GetByUserIdAsync(int userId)
        {
            var accounts = await _context.Accounts.Where(a => a.UserId == userId).ToListAsync();
            return accounts;
        }

        public async Task UpdateBalanceAsync(int id, decimal balance)
        {
            var account = await _context.Accounts.FindAsync(id);
            account.Balance = balance;
            await _context.SaveChangesAsync();
        }

        public async Task<Account> GetByAccountNumberAsync(string accountNumber)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
            return account;
        }
    }
}
