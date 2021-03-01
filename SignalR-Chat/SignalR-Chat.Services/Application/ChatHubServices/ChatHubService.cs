using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using SignalR_Chat.Models;
using SignalR_Chat.Services.Application.ChatHubServices.Commands;
using SignalR_Chat.Services.Hubs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR_Chat.Services.Application.ChatHubServices
{
    public class ChatHubService : IChatHubService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHubContext<ChatHub> _hubClientContext;

        public ChatHubService(IServiceScopeFactory scopeFactory, IHubContext<ChatHub> hubClientContext)
        {
            _scopeFactory = scopeFactory;
            _hubClientContext = hubClientContext;
        }

        public Dictionary<string, int> _connectedUsers { get; private set; } = new Dictionary<string, int>();

        public string GetConnectionIdByUserId(int userId) => _connectedUsers.First(p => p.Value == userId).Key;

        public User GetUserById(int userId)
        {
            var command = new GetUserCommand(_scopeFactory);
            return command.Execute(userId);
        }

        public void OnConnected(string connectionId, int userId) => _connectedUsers.Add(connectionId, userId);

        public void OnDisconnected(string connectionId) => _connectedUsers.Remove(connectionId);

        public void SaveMessage(int userDestinationId, int userSenderId, string message)
        {
            var command = new AddMessageCommand(_scopeFactory);
            command.Execute(userDestinationId, userSenderId, message);
        }

        public async Task SendMessage(int userDestinationId, int userSenderId, string message)
        {
            string connectionId = GetConnectionIdByUserId(userDestinationId);
            User userSender = GetUserById(userSenderId);
            SaveMessage(userDestinationId, userSenderId, message);
            await _hubClientContext.Clients.Client(connectionId).SendAsync("ReceiveMessage", userSender.Name, message);
        }

        public async Task InitTyping(int userSenderId)
        {
            User userSender = GetUserById(userSenderId);
            IReadOnlyList<string> clients = GetClientsConnectionIdExcpetId(userSenderId);
            if (clients.Any())
                await _hubClientContext.Clients.Clients(clients).SendAsync("InitTyping", userSender.Id, userSender.Name);
        }

        public async Task FinishTyping(int userSenderId)
        {
            IReadOnlyList<string> clients = GetClientsConnectionIdExcpetId(userSenderId);
            if (clients.Any())
                await _hubClientContext.Clients.Clients(clients).SendAsync("FinishTyping", userSenderId);
        }

        private IReadOnlyList<string> GetClientsConnectionIdExcpetId(int id) =>
            _connectedUsers.Where(p => p.Value != id).Select(p => p.Key).ToList().AsReadOnly();
    }
}
