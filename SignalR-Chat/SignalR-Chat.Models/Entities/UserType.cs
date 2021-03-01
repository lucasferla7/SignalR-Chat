using SignalR_Chat.Crosscutting.Enums;
using SignalR_Chat.Models.Base;
using System.Collections.Generic;

namespace SignalR_Chat.Models.Entities
{
    public class UserType : BaseEntity
    {
        private UserType(UserTypeEnum @enum)
        {
            Id = (int)@enum;
            Name = @enum.ToString();
        }

        public UserType() { }
        public string Name { get; set; }

        public static implicit operator UserType(UserTypeEnum @enum) => new UserType(@enum);
        public static implicit operator UserTypeEnum(UserType userType) => (UserTypeEnum)userType.Id;

        public IEnumerable<User> Users { get; set; } 
    }
}
