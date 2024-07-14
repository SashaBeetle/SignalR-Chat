using Microsoft.EntityFrameworkCore;
using SignalRChat_backend.Data.Entities;
using SignalRChat_backend.Data.Interfaces;
using SignalRChat_backend.Services.Interfaces;

namespace SignalRChat_backend.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IDbEntityService<User> _userService;
        private readonly IUserDbService _userDbService;

        public UserService(IDbEntityService<User> userService, IUserDbService userDbService)
        {
            _userService = userService;
            _userDbService = userDbService;
        }
        public async Task<User> CreateUserAsync(string name)
        {
            try
            {
                User user = new User
                {
                    Name = name
                };

                await _userService.Create(user);

                return user;
            }
            catch (Exception ex) 
            {
                throw new ServiceException($"User cannot be create", ex);
            }
        }

        public async Task DeleteUserByIdAsync(int userId)
        {
            User user = await _userService.GetById(userId) ?? throw new ServiceException($"User with Id: {userId} not found");

            await _userService.Delete(user);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                IList<User> users = await _userService.GetAll().ToListAsync();

                return users;
            }
            catch (Exception ex) 
            {
                throw new ServiceException($"No users found: ", ex);
            }

        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            try
            {
                User user = await _userDbService.GetUserByIdAsync(userId);

                return user;
            }
            catch (Exception ex)
            {
                throw new ServiceException($"User with Id: {userId} not found", ex);
            }
        }
    }
}
