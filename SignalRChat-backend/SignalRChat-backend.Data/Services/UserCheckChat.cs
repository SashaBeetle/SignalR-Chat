using Microsoft.EntityFrameworkCore;
using SignalRChat_backend.Data.Entities;
using SignalRChat_backend.Data.Interfaces;

namespace SignalRChat_backend.Data.Services
{
    public class UserCheckChat : IUserCheckChat
    {
        private readonly SignalRChatDbContext _dbContext;

        public UserCheckChat(SignalRChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CheckChatInUser(int userId, int chatId)
        {
            //User user = await _dbContext.Set<User>()
            //            .Include(i => i.Chats
            //                .Where(i => i.Id == chatId)
            //                )
            //            .FirstOrDefaultAsync(o => o.Id == userId)
            //            ?? throw new Exception($"User not found");

            //if(user.Chats.Count is 0)
            //{
            //    throw new Exception("Chat incorrect");
            //}

        }
    }
}
