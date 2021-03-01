using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignalR_Chat.Models.Entities;
using SignalR_Chat.Crosscutting.Extensions;
using SignalR_Chat.Crosscutting.Enums;

namespace SignalR_Chat.Data.Mapping
{
    public class UserTypeMapping : IEntityTypeConfiguration<UserType>
    {
        public void Configure(EntityTypeBuilder<UserType> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(32);

            builder.HasMany(p => p.Users).WithOne(p => p.UserType).HasForeignKey(p => p.UserTypeId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasData(
                new UserType
                {
                    Id = (int)UserTypeEnum.system,
                    Name = UserTypeEnum.system.GetEnumDescription()
                },
                new UserType
                {
                    Id = (int)UserTypeEnum.user,
                    Name = UserTypeEnum.user.GetEnumDescription()
                }
            );
        }
    }
}
