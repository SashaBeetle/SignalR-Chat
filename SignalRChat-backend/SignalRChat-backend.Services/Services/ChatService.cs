using Microsoft.EntityFrameworkCore;
using SignalRChat_backend.Data.Entities;
using SignalRChat_backend.Data.Interfaces;
using SignalRChat_backend.Services.Interfaces;

namespace SignalRChat_backend.Services.Services
{
    public class ChatService : IChatService
    {
        private readonly IDbEntityService<Chat> _chatService;

        public ChatService(IDbEntityService<Chat> chatService)
        {
            _chatService = chatService;
        }
        public async Task<Chat> CreateChatAsync(string name, int userId)
        {
            Chat chat = new Chat
            {
                Name = name,
            };

            await _chatService.Create(chat);

            return chat;
        }

        public async Task DeleteChatAsync(int chatId)
        {
            Chat chat = await _chatService.GetById(chatId) ?? throw new Exception($"Chat with Id: {chatId} not found");

            await _chatService.Delete(chat);
        }

        public async Task<IEnumerable<Chat>> GetAllChatsAsync()
        {
            IList<Chat> chats = await _chatService.GetAll().ToListAsync();

            return chats;
        }

        public async Task<Chat> GetChatByIdAsync(int chatId)
        {
            Chat chat = await _chatService.GetById(chatId) ?? throw new Exception($"Chat with Id: {chatId} not found");

            return chat;
        }
    }
}
