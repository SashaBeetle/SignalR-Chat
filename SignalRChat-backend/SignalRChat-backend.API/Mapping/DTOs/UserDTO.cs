using SignalRChat_backend.Data.Entities;

namespace SignalRChat_backend.API.Mapping.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Chat>? Chats { get; set; } = default!;
    }
}
