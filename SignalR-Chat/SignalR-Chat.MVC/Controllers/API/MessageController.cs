using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalR_Chat.Services.Application.MessageServices;
using System.Linq;

namespace SignalR_Chat.MVC.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService) => _messageService = messageService;
        
        [HttpGet]
        [Route("getAllMessagesByUserId/{userReceiverId}")]
        public IActionResult GetAllMessagesByUserId(int userReceiverId) => Ok(_messageService.GetAllMessagesByUserIds(GetLogedUserId(), userReceiverId));

        private int GetLogedUserId() => int.Parse(User.Claims.First(p => p.Type == "UserId").Value);
    }
}
