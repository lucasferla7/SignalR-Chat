using System;
using System.ComponentModel;
using System.Linq;

namespace SignalR_Chat.Crosscutting.Extensions
{
    public static class EnumExtensions
    {
        public static string GetEnumDescription(this Enum item)
            => item.GetType()
                .GetField(item.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .Cast<DescriptionAttribute>()
                .FirstOrDefault()?.Description ?? string.Empty;
    }
}
