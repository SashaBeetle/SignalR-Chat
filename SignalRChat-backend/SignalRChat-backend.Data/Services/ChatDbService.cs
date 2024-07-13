using Microsoft.EntityFrameworkCore;
using SignalRChat_backend.Data.Entities;
using SignalRChat_backend.Data.Interfaces;

namespace SignalRChat_backend.Data.Services
{
    public class ChatDbService : IChatDbService
    {
        private readonly SignalRChatDbContext _dbContext;
        public ChatDbService(SignalRChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Chat>> ChatSearchByNameAsync(string chatName)
        {
            IEnumerable<Chat> chats = await _dbContext.Chats
                                    .Where(c => c.Name.Contains(chatName))
                                    .ToListAsync()
                                    ?? throw new Exception("No such chats found");

            return chats;
        }
        public async Task AddUserToChatAsync(int chatId, int userId)
        {
            UserChat userChat = new UserChat()
            {
                ChatId = chatId,
                UserId = userId
            };

            _dbContext.UsersChats.Add(userChat);
            await _dbContext.SaveChangesAsync();
        }
        public async Task RemoveUserFromChatAsync(int chatId, int userId)
        {
            UserChat userChat = await _dbContext.UsersChats
                                    .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.ChatId == chatId);

            _dbContext.UsersChats.Remove(userChat);
            await _dbContext.SaveChangesAsync();
        }
        public async Task RemoveUsersFromChatAsync(int chatId)
        {
            //Chat chat = await _dbContext.Chats
            //                    .Include(i => i.UserChats)
        }

    }
}
