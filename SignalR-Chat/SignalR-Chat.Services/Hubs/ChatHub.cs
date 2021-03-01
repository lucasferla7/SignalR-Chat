using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Linq;
using SignalR_Chat.Services.Application.ChatHubServices;

namespace SignalR_Chat.Services.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IChatHubService _chatHubService;

        public ChatHub(IChatHubService chatHubService) => _chatHubService = chatHubService;

        public override Task OnConnectedAsync()
        {
            _chatHubService.OnConnected(Context.ConnectionId, GetLogedUserId());
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _chatHubService.OnDisconnected(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessageToUser(string message, int userDestinationId) => 
            await _chatHubService.SendMessage(userDestinationId, GetLogedUserId(), message);

        public async Task InitTyping() =>
            await _chatHubService.InitTyping(GetLogedUserId());

        public async Task FinishTyping() =>
            await _chatHubService.FinishTyping(GetLogedUserId());

        private int GetLogedUserId()
        {
            ClaimsIdentity claims = (ClaimsIdentity)Context.User.Identity;
            int userId = int.Parse(claims.Claims.First(p => p.Type == "UserId").Value);
            return userId;
        }
    }
}
