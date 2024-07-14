using Microsoft.EntityFrameworkCore;
using SignalRChat_backend.Data.Entities;
using SignalRChat_backend.Data.Interfaces;
using SignalRChat_backend.Services.Interfaces;
using System.Xml.Linq;


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
            try
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
            catch (Exception ex)
            {
                throw new ServiceException($"Message cannot be create", ex);
            }
        }

        public async Task DeleteMessageByIdAsync(int messageId)
        {
            Message message = await _messageService.GetById(messageId) ?? throw new ServiceException($"Message with Id: {messageId} not found");

            await _messageService.Delete(message);
        }

        public async Task<IEnumerable<Message>> GetAllMessagesAsync()
        {
            try
            {
                IList<Message> messages = await _messageService.GetAll().ToListAsync();

                return messages;
            }
            catch (Exception ex)
            {
                throw new ServiceException($"No messages found: ", ex);
            }     
        }

        public async Task<Message> GetMessageByIdAsync(int messageId)
        {
            Message message = await _messageService.GetById(messageId) ?? throw new ServiceException($"Message with Id: {messageId} not found");

            return message;
        }
    }
}
