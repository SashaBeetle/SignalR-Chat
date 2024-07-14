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
        public async Task AddUserToChatAsync(int chatId, int userId, string connectionId)
        {
            UserChat userChat = await _dbContext.UsersChats.FirstOrDefaultAsync(f => f.UserId == userId && f.ChatId == chatId);
            
            if (userChat is null)
            {
                UserChat newUserChat = new UserChat()
                {
                    ChatId = chatId,
                    UserId = userId,
                    ConnectionId = connectionId
                };

                _dbContext.UsersChats.Add(newUserChat);
            }
            else
            {
                userChat.ConnectionId = connectionId;
                _dbContext.UsersChats.Update(userChat);
            }

            await _dbContext.SaveChangesAsync();
        }
        public async Task RemoveUserFromChatAsync(int chatId, int userId, string connectionId)
        {
            UserChat userChat = await _dbContext.UsersChats
                                    .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.ChatId == chatId && uc.ConnectionId == connectionId)
                                    ?? throw new Exception("Not found such connections");

            _dbContext.UsersChats.Remove(userChat);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<UserChat>> RemoveUsersFromChatAsync(int chatId)
        {
            IEnumerable<UserChat> Chats = await _dbContext.UsersChats
                .Where(w => w.ChatId == chatId)
                .ToListAsync()
                ?? throw new Exception("Not found such connections");

            foreach (UserChat chat in Chats){
                _dbContext.UsersChats.Remove(chat);
            }
            await _dbContext.SaveChangesAsync();

            return Chats;
        }
        public async Task<Chat> GetChatByIdAsync(int chatId)
        {
            Chat chat = await _dbContext.Set<Chat>()
                                        .Include(x => x.Messages)
                                        .Include(i => i.UserChats).FirstOrDefaultAsync(f => f.Id == chatId)
                                        ?? throw new Exception($"Chat with Id: {chatId} not found");

            return chat;
        }
    }
}
