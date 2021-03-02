using SignalR_Chat.Models;
using SignalR_Chat.Services.Application.BaseService;
using System.Collections.Generic;

namespace SignalR_Chat.Services.Application.MessageServices
{
    public interface IMessageService : IBaseService<Message>
    {
        IEnumerable<Message> GetAllMessagesByUserIds(int userSenderId, int userReceiverId);
    }
}
