using Microsoft.AspNetCore.Mvc;
using SignalRChat_backend.Data.Entities;
using SignalRChat_backend.Services.Interfaces;

namespace SignalRChat_backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<IEnumerable<Message>> GetMessages()
        {
            return await _messageService.GetAllMessagesAsync();
        }

        [HttpGet("{id}")]
        public async Task<Message> GetMessage(int id)
        {
            return await _messageService.GetMessageByIdAsync(id);
        }

        [HttpPost]
        public async Task<Message> CreateMessage([FromBody] string text, int userId, int chatId)
        {
            return await _messageService.CreateMessageAsync(text, userId, chatId);
        }

        [HttpDelete("{id}")]
        public async Task DeleteMessage(int id)
        {
            await _messageService.DeleteMessageByIdAsync(id);
        }
    }
}
