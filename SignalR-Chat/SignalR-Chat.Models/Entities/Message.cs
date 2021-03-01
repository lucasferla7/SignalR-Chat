using SignalR_Chat.Models.Base;
using System;

namespace SignalR_Chat.Models
{
    public class Message : BaseEntity
    {
        public string Text { get; set; }
        public int UserSenderId { get; set; }
        public int UserReceiverId { get; set; }
        public DateTime SendDate { get; set; }
        public User UserSender { get; set; }
        public User UserReceiver { get; set; }
    }
}
