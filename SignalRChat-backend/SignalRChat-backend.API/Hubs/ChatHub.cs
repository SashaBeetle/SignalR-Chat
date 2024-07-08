using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRChat_backend.Data;
using SignalRChat_backend.Data.Entities;

namespace SignalRChat_backend.API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly SignalRChatDbContext _context;

        public ChatHub(SignalRChatDbContext context)
        {
            _context = context;
        }

        public async Task SendMessage(int chatId, int userId, string message)
        {
            var chat = await _context.Chats.Include(c => c.Messages).FirstOrDefaultAsync(c => c.Id == chatId);
            if (chat == null) return;

            var user = await _context.Users.FindAsync(userId);
            if (user == null) return;

            var newMessage = new Message
            {
                ChatId = chatId,
                UserId = userId,
                Text = message,
            };

            _context.Messages.Add(newMessage);
            await _context.SaveChangesAsync();

            await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", user.Name, message);

        }
        public async Task JoinChat(int chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
        }

        public async Task LeaveChat(int chatId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
        }
    }
}
