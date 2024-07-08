using Microsoft.AspNetCore.Mvc;
using SignalRChat_backend.Data.Entities;
using SignalRChat_backend.Services.Interfaces;

namespace SignalRChat_backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet]
        public async Task<IEnumerable<Chat>> GetChats()
        {
            return await _chatService.GetAllChatsAsync();
        }

        [HttpGet("{id}")]
        public async Task<Chat> GetChat(int id)
        {
            return await _chatService.GetChatByIdAsync(id);
        }

        [HttpPost]
        public async Task<Chat> CreateChat([FromBody] string name, int userId)
        {
            return await _chatService.CreateChatAsync(name, userId);
        }

        [HttpDelete("{id}")]
        public async Task DeleteChat(int id)
        {
            await _chatService.DeleteChatAsync(id);
        }
    }
}
