using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;
using SignalR_Chat.Services.ChatHub;

namespace SignalR_Chat.MVC.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatHubService _chatHubService;

        public ChatHub(IChatHubService chatHubService)
        {
            _chatHubService = chatHubService;
        }

        public override Task OnConnectedAsync()
        {
            string connectionId = Context.ConnectionId;
            _chatHubService.OnConnected(connectionId);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            string connectionId = Context.ConnectionId;
            _chatHubService.OnDisconnected(connectionId);

            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string user, string message, int userDestinationId)
        {
            string connectionId = _chatHubService.GetConnectionIdByUserId(userDestinationId);
            await Clients.Client(connectionId).SendAsync("ReceiveMessage", user, message);
        }
    }
}