using SignalRChat_backend.Data.Entities;

namespace SignalRChat_backend.Services.Interfaces
{
    public interface IChatService
    {
        Task<Chat> CreateChatAsync(string name, int userId);
        Task<Chat> GetChatByIdAsync(int chatId);
        Task<IEnumerable<Chat>> GetAllChatsAsync();
        Task<IEnumerable<Chat>> SearchChatsByNameAsync(string chatName);
        Task DeleteChatByIdAsync(int chatId, int userId);
        Task AddUserToChatAsync(int chatId, int userId);
        Task RemoveUserFromChatAsync(int chatId, int userId);
    }
}
