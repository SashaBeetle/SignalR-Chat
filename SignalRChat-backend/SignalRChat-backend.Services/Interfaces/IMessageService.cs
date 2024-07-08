using SignalRChat_backend.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
