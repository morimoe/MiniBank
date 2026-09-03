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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Test User", Email = "test@minibank.com", PasswordHash = "..." }
            );

            modelBuilder.Entity<Account>().HasData(
                new Account { Id = 1, UserId = 1, AccountNumber = "MB-000001", Currency = "MDL", Balance = 1000m }
            );
        }
    }
}