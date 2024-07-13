using Microsoft.AspNetCore.SignalR;
using SignalRChat_backend.Data;
using SignalRChat_backend.Services.Interfaces;

namespace SignalRChat_backend.API.Hubs
{
    public class ChatHub : Hub // Добавити обробку виключень!
    {
        private readonly IChatService _chatService;
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(IChatService chatService, ILogger<ChatHub> logger)
        {
            _chatService = chatService;
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation("Client connected: " + Context.ConnectionId);

            await Clients.All.SendAsync(Context.ConnectionId + " Has already connected");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _logger.LogInformation("Client disconnected: " + Context.ConnectionId);

            await Clients.All.SendAsync(Context.ConnectionId + " Has already disconnected");
            await base.OnDisconnectedAsync(exception);
        }
        public async Task JoinChat(int chatId, int userId)
        {
            _logger.LogInformation("Client joined: " + Context.ConnectionId);

            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
            await _chatService.AddUserToChatAsync(chatId, userId);
            await Clients.Group(chatId.ToString()).SendAsync("Client Joined:", Context.ConnectionId, chatId);
        }
        public async Task LeaveChat(int chatId, int userId)
        {
            _logger.LogInformation("Client leaved: " + Context.ConnectionId);

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
            await _chatService.RemoveUserFromChatAsync(chatId, userId);
            await Clients.Group(chatId.ToString()).SendAsync("ChatLeaved:", Context.ConnectionId, chatId);
        }
        public async Task SendMessage(string message, int chatId)
            => await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", message);
    }
}
