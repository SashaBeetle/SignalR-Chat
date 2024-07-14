using Microsoft.AspNetCore.SignalR;
using SignalRChat_backend.Data.Entities;
using SignalRChat_backend.Services.Interfaces;
using System;

namespace SignalRChat_backend.API.Hubs
{
    public class ChatHub : Hub
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
            try
            {
                _logger.LogInformation("Client connected: " + Context.ConnectionId);

                await Clients.All.SendAsync(Context.ConnectionId + " Has already connected");
                await base.OnConnectedAsync();
            }
            catch (Exception ex) 
            {
                _logger.LogWarning($"User {Context.ConnectionId} cannot connect: ", ex);

                throw new HubException($"User {Context.ConnectionId} cannot connect: ", ex);
            }
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            try
            {
                _logger.LogInformation("Client disconnected: " + Context.ConnectionId);

                await Clients.All.SendAsync(Context.ConnectionId + " Has already disconnected");
                await base.OnDisconnectedAsync(exception);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"User {Context.ConnectionId} cannot disconnect: ", ex);

                throw new HubException($"User {Context.ConnectionId} cannot disconnect: ", ex);
            }
            
        }
        public async Task JoinChat(int chatId, int userId)
        {
            try
            {
                _logger.LogInformation("Client joined: " + Context.ConnectionId);

                await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
                await _chatService.AddUserToChatAsync(chatId, userId, Context.ConnectionId);
                await Clients.Group(chatId.ToString()).SendAsync("Client Joined:", Context.ConnectionId, chatId);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"User {Context.ConnectionId} cannot join to the chat: ", ex);

                throw new HubException($"User {Context.ConnectionId} cannot join to the chat: ", ex);
            }
            
        }
        public async Task LeaveChat(int chatId, int userId)
        {
            try
            {
                _logger.LogInformation("Client leaved: " + Context.ConnectionId);

                await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
                await _chatService.RemoveUserFromChatAsync(chatId, userId, Context.ConnectionId);
                await Clients.Group(chatId.ToString()).SendAsync("ChatLeaved:", Context.ConnectionId, chatId);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"User {Context.ConnectionId} cannot leave the chat: ", ex);

                throw new HubException($"User {Context.ConnectionId} cannot leave the chat: ", ex);
            }
            
        }
        public async Task SendMessage(string message, int chatId)
        {
            try
            {
                _logger.LogInformation($"Client {Context.ConnectionId} send the message: " + message);

                await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", message);

            }
            catch (Exception ex)
            {
                _logger.LogWarning($"User {Context.ConnectionId} cannot send the message: ", ex);

                throw new HubException($"User {Context.ConnectionId} cannot send the message: ", ex);
            }
        }
    }
}
