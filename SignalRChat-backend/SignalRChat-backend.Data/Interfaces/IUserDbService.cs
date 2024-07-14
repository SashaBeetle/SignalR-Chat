using SignalRChat_backend.Data.Entities;

namespace SignalRChat_backend.Data.Interfaces
{
    public interface IUserDbService
    {
        Task<User> GetUserByIdAsync(int userId);
    }
}
