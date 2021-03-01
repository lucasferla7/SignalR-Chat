using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignalR_Chat.Models;

namespace SignalR_Chat.Data.Mapping
{
    public class MessageMapping : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Text).HasMaxLength(256);

            builder.HasOne(p => p.UserReceiver).WithMany(p => p.ReceivedMessages).HasForeignKey(p => p.UserReceiverId).OnDelete(DeleteBehavior.ClientNoAction);
            builder.HasOne(p => p.UserSender).WithMany(p => p.SendedMessages).HasForeignKey(p => p.UserSenderId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
