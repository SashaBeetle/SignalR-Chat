using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat_backend.Data.Interfaces
{
    public interface IUserCheckChat
    {
        Task CheckChatInUser(int userId, int chatId);
    }
}
