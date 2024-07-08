using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat_backend.Data.Entities
{
    public class DbItem 
    {
        public int Id { get; set; }
        public override string ToString()
        {
            return $"Id = {Id}, Type = {GetType().Name}";
        }
    }
}
