using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SignalRChat_backend.API.Mapping.DTOs;
using SignalRChat_backend.Services.Interfaces;
using SignalRChat_backend.Services.Services;

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
            try
            {
                return Ok(_mapper.Map<IEnumerable<MessageDTO>>(await _messageService.GetAllMessagesAsync()));
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessage(int id)
        {
            try
            {
                return Ok(_mapper.Map<MessageDTO>(await _messageService.GetMessageByIdAsync(id)));
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

        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromBody] string text, int userId, int chatId)
        {
            try
            {
                return Ok(_mapper.Map<MessageDTO>(await _messageService.CreateMessageAsync(text, userId, chatId)));
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
        public async Task<IActionResult> DeleteMessage(int id)
        {
            try
            {
                await _messageService.DeleteMessageByIdAsync(id);
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
