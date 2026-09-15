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
            var normalizedEmail = email.ToLower();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == normalizedEmail);
            return user;
        }
        public async Task<User?> GetByUsernameAsync(string username)
        {
            var normalizedUsername = username.ToLower();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name.ToLower() == normalizedUsername);
            return user;
        }
        public async Task<User?> GetByIdAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }
    }
}
