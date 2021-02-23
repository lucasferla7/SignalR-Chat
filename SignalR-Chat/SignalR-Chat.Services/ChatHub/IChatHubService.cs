namespace SignalR_Chat.Services.ChatHub
{
    public interface IChatHubService
    {
        void OnConnected(string connectionId);
        void OnDisconnected(string connectionId);
        string GetConnectionIdByUserId(int userId);
    }
}
