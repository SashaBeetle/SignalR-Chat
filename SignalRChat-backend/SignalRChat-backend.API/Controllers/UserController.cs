using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SignalRChat_backend.API.Mapping.DTOs;
using SignalRChat_backend.Services.Interfaces;


namespace SignalRChat_backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(_mapper.Map<IEnumerable<UserDTO>>(await _userService.GetAllUsersAsync()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            return Ok(_mapper.Map<UserDTO>(await _userService.GetUserByIdAsync(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] string name)
        {
            return Ok(_mapper.Map<UserDTO>(await _userService.CreateUserAsync(name)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChat(int id)
        {
            await _userService.DeleteUserByIdAsync(id);
            return NoContent();
        }

        [HttpPost]
        [Route("testing")]
        public async Task<IActionResult> CheckUser(int userId, int chatId)
        {
            await _userService.CheckChatForUser(userId, chatId);
            return NoContent();
        }
    }
}
