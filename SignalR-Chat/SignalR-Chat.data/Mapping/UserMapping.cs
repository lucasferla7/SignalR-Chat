using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignalR_Chat.Crosscutting.Enums;
using SignalR_Chat.Models;
using System;

namespace SignalR_Chat.Data.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Name).HasMaxLength(128);
            builder.Property(p => p.UserTypeId).HasDefaultValue((int)UserTypeEnum.user);
            builder.Ignore(p => p.Age);

            builder.HasMany(p => p.ReceivedMessages).WithOne(p => p.UserReceiver).HasForeignKey(p => p.UserReceiverId).OnDelete(DeleteBehavior.ClientNoAction);
            builder.HasMany(p => p.SendedMessages).WithOne(p => p.UserSender).HasForeignKey(p => p.UserSenderId).OnDelete(DeleteBehavior.ClientNoAction);
            builder.HasOne(p => p.UserType).WithMany(p => p.Users).HasForeignKey(p => p.UserTypeId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasData(
                new User
                {
                    Id = 1,
                    Name = "Sistema",
                    BirthDate = new DateTime(1992, 03, 26),
                    UserTypeId = (int)UserTypeEnum.system
                },
                new User
                {
                    Id = 2,
                    Name = "Mathieu",
                    BirthDate = new DateTime(1995, 04, 24),
                    UserTypeId = (int)UserTypeEnum.user
                },
                new User
                {
                    Id = 3,
                    Name = "José",
                    BirthDate = new DateTime(1998, 10, 26),
                    UserTypeId = (int)UserTypeEnum.user
                },
                new User
                {
                    Id = 4,
                    Name = "Jurassic Park",
                    BirthDate = new DateTime(1994, 10, 26),
                    UserTypeId = (int)UserTypeEnum.user
                }
            ); ;
        }
    }
}
