using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalR_Chat.Services.Application.SchedulerServices;
using System;

namespace SignalR_Chat.MVC.Controllers
{
    [Authorize]
    public class SchedulerController : Controller
    {
        private readonly ISchedulerMessageService _scheduleMessageService;

        public SchedulerController(ISchedulerMessageService scheduleMessageService)
        {
            _scheduleMessageService = scheduleMessageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("scheduleMessageForUserId/{id}")]
        public IActionResult ScheduleMessageForUserId(int id, string message, DateTime scheduleDate)
        {
            _scheduleMessageService.ScheduleMessageToUser(id, message, scheduleDate);

            return Ok();
        }
    }
}
