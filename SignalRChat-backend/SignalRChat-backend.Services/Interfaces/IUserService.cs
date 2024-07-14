using SignalRChat_backend.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
