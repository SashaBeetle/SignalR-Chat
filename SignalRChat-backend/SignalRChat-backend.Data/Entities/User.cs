namespace SignalRChat_backend.Data.Entities
{
    public class User : DbItem
    {
        public string Name { get; set; }
        public ICollection<UserChat> UserChats { get; set; }
    }
}
