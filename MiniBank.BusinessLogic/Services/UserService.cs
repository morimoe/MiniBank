using MiniBank.BusinessLogic.Entities;
using MiniBank.BusinessLogic.DTO;
using MiniBank.BusinessLogic.Ports;

namespace MiniBank.BusinessLogic.Services
{
    public class UserService: IUserService
    {
        public readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var account = await _userRepository.GetByIdAsync(id);
            if (account == null) { return null; }
            return new UserDto { Name = account.Name, Email = account.Email };
        }
    }
}
