using Microsoft.EntityFrameworkCore;
using MiniBank.BusinessLogic.Entities;
using MiniBank.BusinessLogic.Ports;
using MiniBank.DataAccess.Context;

namespace MiniBank.DataAccess.Adapters
{
    public class UserRepository : IUserRepository
    {
        private readonly MiniBankDbContext _context;
        public UserRepository(MiniBankDbContext context)
        {
            _context = context;
        }
        public async Task<User?> GetByEmailAsync(string email)
        { 
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
    }
}
