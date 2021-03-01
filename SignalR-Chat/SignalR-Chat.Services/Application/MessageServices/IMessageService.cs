namespace SignalR_Chat.Services.Application.MessageServices
{
    public interface IMessageService
    {
        public void SaveMessage(int userDestinationId, int userSenderId, string message);
    }
}
