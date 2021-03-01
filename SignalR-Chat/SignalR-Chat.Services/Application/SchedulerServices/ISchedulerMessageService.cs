using System;
using System.Threading.Tasks;

namespace SignalR_Chat.Services.Application.SchedulerServices
{
    public interface ISchedulerMessageService
    {
        void ScheduleMessageToUser(int userId, string message, DateTime scheduleTime);
    }
}
