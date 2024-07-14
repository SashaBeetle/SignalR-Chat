using Microsoft.EntityFrameworkCore;
using SignalRChat_backend.Data.Entities;
using SignalRChat_backend.Data.Interfaces;
using SignalRChat_backend.Services.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;

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
            try
            {
                Chat chat = new Chat
                {
                    Name = name,
                    CreatorId = userId
                };

                await _chatService.Create(chat);

                return chat;
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Chat cannot be create", ex);
            }
            
        }
        public async Task DeleteChatByIdAsync(int chatId, int userId)
        {
            Chat chat = await _chatService.GetById(chatId) ?? throw new ServiceException($"Chat with Id: {chatId} not found");

            if (chat.CreatorId != userId)
                throw new ServiceException($"There are no permissions to do the operation");

            await _chatService.Delete(chat);
        }
        public async Task<IEnumerable<Chat>> GetAllChatsAsync()
        {
            try
            {
                IList<Chat> chats = await _chatService.GetAll().ToListAsync();

                return chats;
            }
            catch (Exception ex)
            {
                throw new ServiceException($"No chats found: ", ex);
            }
        }
        public async Task<Chat> GetChatByIdAsync(int chatId)
        {
            try
            {
                Chat chat = await _chatDbService.GetChatByIdAsync(chatId);

                return chat;
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Chat not found: ", ex);
            }
            
        }
        public async Task<IEnumerable<Chat>> SearchChatsByNameAsync(string chatName)
        {
            try
            {
                IEnumerable<Chat> chats = await _chatDbService.ChatSearchByNameAsync(chatName);

                return chats;
            }
            catch (Exception ex)
            {
                throw new ServiceException($"No chats found: ", ex);
            }
        }
        public async Task AddUserToChatAsync(int chatId, int userId,string connectionId)
        {
            try
            {
                await _chatDbService.AddUserToChatAsync(chatId, userId, connectionId);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"The operation has not been completed: ", ex);
            }
        }
        public async Task RemoveUserFromChatAsync(int chatId, int userId, string connectionId)
        {
            try
            {
                await _chatDbService.RemoveUserFromChatAsync(chatId, userId, connectionId);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"The operation has not been completed: ", ex);
            }
        }
        public async Task<IEnumerable<UserChat>> RemoveUsersFromChatAsync(int chatId)
        {
            try
            {
                IEnumerable<UserChat> chats = await _chatDbService.RemoveUsersFromChatAsync(chatId);

                return chats;
            }
            catch (Exception ex)
            {
                throw new ServiceException($"The operation has not been completed: ", ex);
            }
            
        }
    }
}
