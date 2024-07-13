using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRChat_backend.API.Hubs;
using SignalRChat_backend.API.Mapping.DTOs;
using SignalRChat_backend.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SignalRChat_backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(IChatService chatService, IMapper mapper, IHubContext<ChatHub> hubContext)
        {
            _chatService = chatService;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetChats()
        {
            return Ok(_mapper.Map<IEnumerable<ChatDTO>>(await _chatService.GetAllChatsAsync()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChat(int id)
        {
            return Ok(_mapper.Map<ChatDTO>( await _chatService.GetChatByIdAsync(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat([FromBody] string name, int userId)
        {
            return Ok(_mapper.Map<ChatDTO>(await _chatService.CreateChatAsync(name, userId)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChat(int chatId, int userId)
        {
            await _chatService.DeleteChatByIdAsync(chatId, userId);

          //var connections = _hubContext.UserChats.Where(uc => uc.ChatId == chatId).ToList();

          //foreach (var connection in connections)
          //{
          //    await _hubContext.Groups.RemoveFromGroupAsync(connection.UserId, chatId.ToString());
          //}

            await _hubContext.Clients.Group(chatId.ToString()).SendAsync("ChatClosed");
            return NoContent();
        }
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> SearchChat([Required] string name)
        {
            return Ok(_mapper.Map<List<ChatDTO>>(await _chatService.SearchChatsByNameAsync(name)));
        }
    }
}
