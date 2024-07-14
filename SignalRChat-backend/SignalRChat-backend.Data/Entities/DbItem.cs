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
