using System.Collections.Generic;
using System.Linq;

namespace SignalR_Chat.Services.ChatHub
{
    public class ChatHubService : IChatHubService
    {
        private Dictionary<string, int> _connectedUsers = new Dictionary<string, int>();

        public string GetConnectionIdByUserId(int userId) => _connectedUsers.First(p => p.Value == userId).Key;

        public void OnConnected(string connectionId)
        {
            int? userId = _connectedUsers.OrderByDescending(p => p.Value).FirstOrDefault().Value;
            _connectedUsers.Add(connectionId, userId.HasValue ? userId.Value + 1 : 1);
        }

        public void OnDisconnected(string connectionId) => _connectedUsers.Remove(connectionId);
    }
}
