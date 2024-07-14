using SignalRChat_backend.Data.Entities;

namespace SignalRChat_backend.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(string name);
        Task<User> GetUserByIdAsync(int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task DeleteUserByIdAsync(int userId);
    }
}
