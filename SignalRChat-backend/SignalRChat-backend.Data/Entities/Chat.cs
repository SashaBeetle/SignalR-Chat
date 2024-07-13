namespace SignalRChat_backend.Data.Entities
{
    public class Chat : DbItem
    {
        public string Name { get; set; }
        public int CreatorId { get; set; }
        public ICollection<UserChat> UserChats { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
