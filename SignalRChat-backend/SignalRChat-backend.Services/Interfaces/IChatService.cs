using SignalRChat_backend.Data.Entities;

namespace SignalRChat_backend.Services.Interfaces
{
    public interface IChatService
    {
        Task<Chat> CreateChatAsync(string name, int userId);
        Task<Chat> GetChatByIdAsync(int chatId);
        Task<IEnumerable<Chat>> GetAllChatsAsync();
        Task DeleteChatAsync(int chatId);
    }
}
