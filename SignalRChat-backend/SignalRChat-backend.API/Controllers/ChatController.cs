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
        public async Task<IActionResult> CreateChat([FromBody] string name,[Required] int userId)
        {
            return Ok(_mapper.Map<ChatDTO>(await _chatService.CreateChatAsync(name, userId)));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteChat([Required]int chatId, [Required] int userId)
        {
            IList<UserChatDTO> userChatDTOos = _mapper.Map<IList<UserChatDTO>>(await _chatService.RemoveUsersFromChatAsync(chatId));

            await _chatService.DeleteChatByIdAsync(chatId, userId);

            await _hubContext.Clients.Group(chatId.ToString()).SendAsync("Chat Closed");

            foreach (var userChatDTO in userChatDTOos)
            {
                await _hubContext.Groups.RemoveFromGroupAsync(userChatDTO.ConnectionId, chatId.ToString());
            }

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
