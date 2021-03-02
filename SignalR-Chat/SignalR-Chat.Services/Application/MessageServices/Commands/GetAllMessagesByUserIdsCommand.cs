using Microsoft.EntityFrameworkCore;
using SignalR_Chat.Models;
using System.Collections.Generic;
using System.Linq;

namespace SignalR_Chat.Services.Application.MessageServices.Commands
{
    public class GetAllMessagesByUserIdsCommand
    {
        private readonly DbSet<Message> _dbSet;

        public GetAllMessagesByUserIdsCommand(DbSet<Message> dbSet) => _dbSet = dbSet;

        public IEnumerable<Message> Execute(int userSenderId, int userReceiverId) =>
            _dbSet.Include(p => p.UserSender).Where(p => (p.UserSenderId.Equals(userSenderId) & p.UserReceiverId.Equals(userReceiverId)) 
                || (p.UserSenderId.Equals(userReceiverId) & p.UserReceiverId.Equals(userSenderId)))
            .OrderBy(p => p.SendDate);
    }
}
