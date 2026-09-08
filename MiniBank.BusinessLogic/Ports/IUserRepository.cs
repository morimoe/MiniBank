using MiniBank.BusinessLogic.Entities;

namespace MiniBank.BusinessLogic.Ports
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
