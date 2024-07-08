using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat_backend.Data.Entities
{
    public class Message : DbItem
    {
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public Chat Chat { get; set; }
        public User User { get; set; }
    }
}
