using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SignalRChat_backend.API.Mapping.DTOs;
using SignalRChat_backend.Services.Interfaces;

namespace SignalRChat_backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public MessageController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {
            return Ok(_mapper.Map<IEnumerable<MessageDTO>>(await _messageService.GetAllMessagesAsync()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessage(int id)
        {
            return Ok(_mapper.Map<MessageDTO>(await _messageService.GetMessageByIdAsync(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromBody] string text, int userId, int chatId)
        {
            return Ok(_mapper.Map<MessageDTO>(await _messageService.CreateMessageAsync(text, userId, chatId)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            await _messageService.DeleteMessageByIdAsync(id);
            return NoContent();
        }
    }
}
