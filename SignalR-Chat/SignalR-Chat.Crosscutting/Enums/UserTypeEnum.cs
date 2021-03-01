using System.ComponentModel;

namespace SignalR_Chat.Crosscutting.Enums
{
    public enum UserTypeEnum
    {
        [Description("User")]
        user = 1,
        [Description("System")]
        system
    }
}
