using SignalRChat_backend.Data.Entities;

namespace SignalRChat_backend.API.Mapping.DTOs
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }
    }
}
