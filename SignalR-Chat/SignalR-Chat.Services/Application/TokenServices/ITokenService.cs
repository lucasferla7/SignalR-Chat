namespace SignalR_Chat.Services.Application.TokenServices
{
    public interface ITokenService
    {
        string GenerateToken(string userId);
    }
}
