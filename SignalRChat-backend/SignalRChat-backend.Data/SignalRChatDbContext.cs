using Microsoft.EntityFrameworkCore;
using SignalRChat_backend.Data.Entities;


namespace SignalRChat_backend.Data
{
    public class SignalRChatDbContext : DbContext
    {
        public SignalRChatDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasMany(u => u.Chats)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Chat>()
                .HasMany(c => c.Messages)
                .WithOne(m => m.Chat)
                .HasForeignKey(m => m.ChatId);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId);
        }
    }
}
