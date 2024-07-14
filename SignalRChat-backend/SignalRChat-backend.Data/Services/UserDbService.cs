using Microsoft.EntityFrameworkCore;
using SignalRChat_backend.Data.Entities;
using SignalRChat_backend.Data.Interfaces;

namespace SignalRChat_backend.Data.Services
{
    public class UserDbService : IUserDbService
    {
        private readonly SignalRChatDbContext _dbContext;
        public UserDbService(SignalRChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> GetUserByIdAsync(int userId)
        {
            User user = await _dbContext.Set<User>()
                            .Include(x => x.UserChats)
                            .FirstOrDefaultAsync(x => x.Id == userId)
                            ?? throw new Exception($"Message with Id: {userId} not found");

            return user;
        }
    }
}
