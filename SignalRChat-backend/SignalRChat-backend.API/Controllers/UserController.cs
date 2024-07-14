using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SignalRChat_backend.API.Mapping.DTOs;
using SignalRChat_backend.Services.Interfaces;
using SignalRChat_backend.Services.Services;


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
            try
            {
                return Ok(_mapper.Map<IEnumerable<UserDTO>>(await _userService.GetAllUsersAsync()));
            }
            catch (ServiceException) {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                return Ok(_mapper.Map<UserDTO>(await _userService.GetUserByIdAsync(id)));
            }
            catch (ServiceException)
            {
                return NotFound();
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] string name)
        {
            try
            {
                return Ok(_mapper.Map<UserDTO>(await _userService.CreateUserAsync(name)));

            }
            catch (ServiceException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChat(int id)
        {
            try
            {
                await _userService.DeleteUserByIdAsync(id);
                return NoContent();
            }
            catch (ServiceException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
