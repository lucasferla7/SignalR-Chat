using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SignalR_Chat.Crosscutting.Enums;
using SignalR_Chat.Data.Context;
using SignalR_Chat.Services.Application.HomeServices;
using System.Linq;

namespace SignalR_Chat.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService _homeService;
        private readonly ChatContext context;

        public HomeController(ILogger<HomeController> logger, IHomeService homeService, ChatContext context)
        {
            _logger = logger;
            _homeService = homeService;
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetUsers()
        {
            var a = context.User.First(p => p.UserTypeId == (int)UserTypeEnum.system);
            return Ok(a);
        }
    }
}
