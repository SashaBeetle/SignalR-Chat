using SignalRChat_backend.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat_backend.Data.Interfaces
{
    public interface IChatDbService
    {
        Task<IEnumerable<Chat>> ChatSearchByNameAsync(string chatName);
        Task AddUserToChatAsync(int chatId, int userId);
        Task RemoveUserFromChatAsync(int chatId, int userId);
        Task RemoveUsersFromChatAsync(int chatId);
    }
}
