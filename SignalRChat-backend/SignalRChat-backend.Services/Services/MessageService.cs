using Microsoft.EntityFrameworkCore;
using SignalRChat_backend.Data.Entities;
using SignalRChat_backend.Data.Interfaces;
using SignalRChat_backend.Services.Interfaces;


namespace SignalRChat_backend.Services.Services
{
    public class MessageService : IMessageService
    {
        private readonly IDbEntityService<Message> _messageService;
        public MessageService(IDbEntityService<Message> messageService)
        {
            _messageService = messageService;
        }

        public async Task<Message> CreateMessageAsync(string text, int userId, int chatId)
        {
            Message message = new Message
            {
                Text = text,
                UserId = userId,
                ChatId = chatId
            };

            await _messageService.Create(message);

            return message;
        }

        public async Task DeleteMessageByIdAsync(int messageId)
        {
            Message message = await _messageService.GetById(messageId) ?? throw new Exception($"Message with Id: {messageId} not found");

            await _messageService.Delete(message);
        }

        public async Task<IEnumerable<Message>> GetAllMessagesAsync()
        {
            IList<Message> messages = await _messageService.GetAll().ToListAsync();

            return messages;
        }

        public async Task<Message> GetMessageByIdAsync(int messageId)
        {
            Message message = await _messageService.GetById(messageId) ?? throw new Exception($"Message with Id: {messageId} not found");

            return message;
        }
    }
}
