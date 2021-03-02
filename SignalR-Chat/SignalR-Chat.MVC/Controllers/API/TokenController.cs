using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalR_Chat.Data.Context;
using SignalR_Chat.Models;
using SignalR_Chat.Services.Application.TokenServices;
using System.Linq;

namespace SignalR_Chat.MVC.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ChatContext _context;
        private readonly ITokenService _tokenService;

        public TokenController(ChatContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [Route("getToken/{userId}")]
        [AllowAnonymous]
        public IActionResult GetToken(int userId)
        {
            User user = _context.User.FirstOrDefault(p => p.Id == userId);

            if (user != null)
                return Ok(_tokenService.GenerateToken(userId.ToString()));

            return BadRequest();            
        }
    }
}
