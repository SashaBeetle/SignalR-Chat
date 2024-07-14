using SignalRChat_backend.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat_backend.Data.Interfaces
{
    public interface IUserDbService
    {
        Task<User> GetUserByIdAsync(int userId);
    }
}
