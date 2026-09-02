using Microsoft.EntityFrameworkCore;
using MiniBank.BusinessLogic.Ports;
using MiniBank.DataAccess.Context;
using MiniBank.BusinessLogic.Entities;

namespace MiniBank.DataAccess.Adapters
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MiniBankDbContext _context;

        public AccountRepository(MiniBankDbContext context)
        {
            _context = context;
        }

        public async Task<Account?> GetByIdAsync(int id) =>
            await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id);

        public async Task<List<Account>> GetByUserIdAsync(int userId) =>
            await _context.Accounts.Where(a => a.UserId == userId).ToListAsync();

        public async Task UpdateAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }
    }
}