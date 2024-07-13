using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat_backend.Data.Entities
{
    public class User : DbItem
    {
        public string Name { get; set; }
        public ICollection<UserChat> UserChats { get; set; }
    }
}
