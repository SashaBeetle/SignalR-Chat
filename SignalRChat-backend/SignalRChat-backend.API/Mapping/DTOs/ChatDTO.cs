namespace SignalRChat_backend.API.Mapping.DTOs
{
    public class ChatDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreatorId { get; set; }
        public string ConnectionId { get; set; }
        public ICollection<MessageDTO>? Messages { get; set; } = default!;
        public List<int> UserIds { get; set; }
    }
}