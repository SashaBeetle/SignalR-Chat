using SignalRChat_backend.Data.Entities;

namespace SignalRChat_backend.Data.Interfaces
{
    public interface IChatDbService
    {
        Task<IEnumerable<Chat>> ChatSearchByNameAsync(string chatName);
        Task<Chat> GetChatByIdAsync(int chatId);
        Task AddUserToChatAsync(int chatId, int userId, string connectionId);
        Task RemoveUserFromChatAsync(int chatId, int userId, string connectionId);
        Task<IEnumerable<UserChat>> RemoveUsersFromChatAsync(int chatId);
    }
}
