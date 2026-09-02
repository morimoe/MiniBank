using Microsoft.EntityFrameworkCore;
using MiniBank.BusinessLogic.Entities;

namespace MiniBank.DataAccess.Context
{
    public class MiniBankDbContext : DbContext
    {
        public MiniBankDbContext(DbContextOptions<MiniBankDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}