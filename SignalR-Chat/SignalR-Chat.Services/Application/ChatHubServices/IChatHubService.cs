using SignalR_Chat.Models;
using System.Threading.Tasks;

namespace SignalR_Chat.Services.Application.ChatHubServices
{
    public interface IChatHubService
    {
        void OnConnected(string connectionId, int userId);
        void OnDisconnected(string connectionId);
        string GetConnectionIdByUserId(int userId);
        User GetUserById(int userId);
        void SaveMessage(int userDestinationId, int userSenderId, string message);
        Task SendMessage(int userDestinationId, int userSenderId, string message);
        Task InitTyping(int userSenderId);
        Task FinishTyping(int userSenderId);
    }
}
