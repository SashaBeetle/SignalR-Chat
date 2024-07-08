using Microsoft.EntityFrameworkCore;
using SignalRChat_backend.Data.Entities;
using SignalRChat_backend.Data.Interfaces;
using SignalRChat_backend.Services.Interfaces;

namespace SignalRChat_backend.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IDbEntityService<User> _userService;

        public UserService(IDbEntityService<User> userService)
        {
            _userService = userService;
        }
        public async Task<User> CreateUserAsync(string name)
        {
            User user = new User
            {
                Name = name
            };

            await _userService.Create(user);

            return user;
        }

        public async Task DeleteUserByIdAsync(int userId)
        {
            User user = await _userService.GetById(userId) ?? throw new Exception($"User with Id: {userId} not found");

            await _userService.Delete(user);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            IList<User> users = await _userService.GetAll().ToListAsync();

            return users;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            User user = await _userService.GetById(userId) ?? throw new Exception($"User with Id: {userId} not found");

            return user;
        }
    }
}
