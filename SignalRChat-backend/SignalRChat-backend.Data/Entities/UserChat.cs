using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat_backend.Data.Entities
{
    public class UserChat
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}
