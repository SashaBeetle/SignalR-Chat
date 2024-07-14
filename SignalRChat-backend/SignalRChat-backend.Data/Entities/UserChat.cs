namespace SignalRChat_backend.Data.Entities
{
    public class UserChat
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public string ConnectionId { get; set; }

        public int ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}
