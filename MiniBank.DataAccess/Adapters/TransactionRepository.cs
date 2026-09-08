using MiniBank.BusinessLogic.Ports;
using MiniBank.BusinessLogic.Entities;
using MiniBank.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace MiniBank.DataAccess.Adapters
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly MiniBankDbContext _context;
        public TransactionRepository(MiniBankDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetByAccountIdAsync(int accountId)
        {
            return await _context.Transactions.Where(t => t.FromAccountId == accountId || t.ToAccountId == accountId).ToListAsync();
        }

        public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }
    }
}
