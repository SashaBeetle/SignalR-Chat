using Microsoft.AspNetCore.Mvc;
using SignalRChat_backend.Data.Entities;
using SignalRChat_backend.Services.Interfaces;
using SignalRChat_backend.Services.Services;

namespace SignalRChat_backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userService.GetAllUsersAsync();
        }

        [HttpGet("{id}")]
        public async Task<User> GetUser(int id)
        {
            return await _userService.GetUserByIdAsync(id);
        }

        [HttpPost]
        public async Task<User> CreateChat([FromBody] string name)
        {
            return await _userService.CreateUserAsync(name);
        }

        [HttpDelete("{id}")]
        public async Task DeleteChat(int id)
        {
            await _userService.DeleteUserByIdAsync(id);
        }
    }
}
