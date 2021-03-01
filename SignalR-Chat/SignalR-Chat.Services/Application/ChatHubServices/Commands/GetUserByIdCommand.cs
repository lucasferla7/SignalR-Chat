using Microsoft.Extensions.DependencyInjection;
using SignalR_Chat.Data.Context;
using SignalR_Chat.Models;

namespace SignalR_Chat.Services.Application.ChatHubServices.Commands
{
    public class GetUserCommand
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public GetUserCommand(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public User Execute(int id)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ChatContext>();
                return db.User.Find(id);
            }
        }
    }
}
