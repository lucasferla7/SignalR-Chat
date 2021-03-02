using SignalR_Chat.Data.Context;
using SignalR_Chat.Models;
using SignalR_Chat.Services.Application.BaseService;
using SignalR_Chat.Services.Application.MessageServices.Commands;
using System.Collections.Generic;

namespace SignalR_Chat.Services.Application.MessageServices
{
    public class MessageService : BaseService<Message>, IMessageService
    {
        public MessageService(ChatContext context) : base(context) { }

        public IEnumerable<Message> GetAllMessagesByUserIds(int userSenderId, int userReceiverId)
        {
            GetAllMessagesByUserIdsCommand command = new GetAllMessagesByUserIdsCommand(_db);
            return command.Execute(userSenderId, userReceiverId);
        }
    }
}
