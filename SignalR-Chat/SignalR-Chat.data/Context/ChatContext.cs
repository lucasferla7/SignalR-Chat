using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SignalR_Chat.Data.Mapping;
using SignalR_Chat.Models;
using SignalR_Chat.Models.Entities;

namespace SignalR_Chat.Data.Context
{
    public class ChatContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<User> User { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<UserType> UserType { get; set; }

        public ChatContext(IConfiguration configuration, DbContextOptions<ChatContext> options) : base(options)
        {
            _configuration = configuration;
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration["ConnectionStrings:Sqlite"];
            optionsBuilder.UseSqlite(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new MessageMapping());
            modelBuilder.ApplyConfiguration(new UserTypeMapping());
        }
    }
}