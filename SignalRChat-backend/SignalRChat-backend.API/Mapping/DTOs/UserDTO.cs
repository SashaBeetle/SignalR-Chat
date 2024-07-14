using SignalRChat_backend.Data.Entities;

namespace SignalRChat_backend.API.Mapping.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> ChatIds { get; set; }
    }
}
