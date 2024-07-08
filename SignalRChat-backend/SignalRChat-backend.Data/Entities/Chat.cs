using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat_backend.Data.Entities
{
    public class Chat : DbItem
    {
        public string Name { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
