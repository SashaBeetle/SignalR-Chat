using SignalRChat_backend.Data.Entities;


namespace SignalRChat_backend.Services.Interfaces
{
    public interface IMessageService
    {
        Task<Message> CreateMessageAsync(string text, int userId, int chatId);
        Task<Message> GetMessageByIdAsync(int messageId);
        Task<IEnumerable<Message>> GetAllMessagesAsync();
        Task DeleteMessageByIdAsync(int messageId);
    }
}
