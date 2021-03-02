using Microsoft.Extensions.DependencyInjection;
using SignalR_Chat.Data.Context;
using System;

namespace SignalR_Chat.Services.Application.ChatHubServices.Commands
{
    public class AddMessageCommand
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public AddMessageCommand(IServiceScopeFactory scopeFactory) =>
            _scopeFactory = scopeFactory;

        public void Execute(int userDestinationId, int userSenderId, string message)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ChatContext>();
            db.Message.Add(new Models.Message
            {
                SendDate = DateTime.Now,
                Text = message,
                UserReceiverId = userDestinationId,
                UserSenderId = userSenderId
            });

            db.SaveChanges();
        }
    }
}
