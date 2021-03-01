using Hangfire;
using SignalR_Chat.Services.Application.ChatHubServices;
using System;

namespace SignalR_Chat.Services.Application.SchedulerServices
{
    public class SchedulerMessageService : ISchedulerMessageService
    {
        private readonly IChatHubService _chatHubService;

        public SchedulerMessageService(IChatHubService chatHubService)
        {
            _chatHubService = chatHubService;
        }

        public void ScheduleMessageToUser(int userId, string message, DateTime scheduleTime)
        {
            BackgroundJob.Schedule(() => _chatHubService.SendMessage(1, 1, message), scheduleTime);
        }
    }
}
