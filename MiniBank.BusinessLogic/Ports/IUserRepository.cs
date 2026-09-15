using MiniBank.BusinessLogic.Entities;

namespace MiniBank.BusinessLogic.Ports
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByIdAsync(int id);
    }
}
