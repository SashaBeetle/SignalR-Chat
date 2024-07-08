using SignalRChat_backend.Data;
using SignalRChat_backend.Data.Entities;
using SignalRChat_backend.Services.Interfaces;

namespace SignalRChat_backend.Services.Services
{
    public class ChatService : IChatService
    {
        private readonly SignalRChatDbContext _dbContext;

        public ChatService(SignalRChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Chat> CreateChatAsync(string name, int userId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteChatAsync(int chatId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Chat>> GetAllChatsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Chat> GetChatByIdAsync(int chatId)
        {
            throw new NotImplementedException();
        }
    }
}
