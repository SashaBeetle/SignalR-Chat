using Microsoft.EntityFrameworkCore;
using SignalRChat_backend.Data.Entities;
using SignalRChat_backend.Data.Interfaces;
using SignalRChat_backend.Services.Interfaces;

namespace SignalRChat_backend.Services.Services
{
    public class ChatService : IChatService
    {
        private readonly IDbEntityService<Chat> _chatService;
        private readonly IChatDbService _chatDbService;

        public ChatService(IDbEntityService<Chat> chatService, IChatDbService chatDbService)
        {
            _chatService = chatService;
            _chatDbService = chatDbService;
        }
        public async Task<Chat> CreateChatAsync(string name, int userId)
        {
            Chat chat = new Chat
            {
                Name = name,
                CreatorId = userId
            };

            await _chatService.Create(chat);

            return chat;
        }
        public async Task DeleteChatByIdAsync(int chatId, int userId)
        {
            Chat chat = await _chatService.GetById(chatId) ?? throw new Exception($"Chat with Id: {chatId} not found");

            if (chat.CreatorId != userId)
                throw new UserException($"There are no permissions to do the operation");

            await _chatService.Delete(chat);
        }
        public async Task<IEnumerable<Chat>> GetAllChatsAsync()
        {
            IList<Chat> chats = await _chatService.GetAll().ToListAsync();

            return chats;
        }
        public async Task<Chat> GetChatByIdAsync(int chatId)
        {
            Chat chat = await _chatDbService.GetChatByIdAsync(chatId) ?? throw new Exception($"Chat with Id: {chatId} not found");

            return chat;
        }
        public async Task<IEnumerable<Chat>> SearchChatsByNameAsync(string chatName)
        {
            IEnumerable<Chat> chats = await _chatDbService.ChatSearchByNameAsync(chatName);

            return chats;
        }
        public async Task AddUserToChatAsync(int chatId, int userId,string connectionId)
        {
            await _chatDbService.AddUserToChatAsync(chatId, userId, connectionId);
        }
        public async Task RemoveUserFromChatAsync(int chatId, int userId, string connectionId)
        {
            await _chatDbService.RemoveUserFromChatAsync(chatId, userId, connectionId);
        }
        public async Task<IEnumerable<UserChat>> RemoveUsersFromChatAsync(int chatId)
        {
            IEnumerable<UserChat> chats = await _chatDbService.RemoveUsersFromChatAsync(chatId);

            return chats;
        }
    }
}
