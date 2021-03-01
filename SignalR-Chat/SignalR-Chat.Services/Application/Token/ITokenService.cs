namespace SignalR_Chat.Services.Application.Token
{
    public interface ITokenService
    {
        string GenerateToken(string userId);
    }
}
