using SignalRChat_backend.Data.Entities;

namespace SignalRChat_backend.API.Mapping.DTOs
{
    public class ChatDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public ICollection<Message>? Messages { get; set; } = default!;
    }
}