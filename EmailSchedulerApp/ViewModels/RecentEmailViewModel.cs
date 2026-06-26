using EmailSchedulerApp.Enums;

namespace EmailSchedulerApp.ViewModels.Dashboard
{
    public class RecentEmailViewModel
    {
        public int ScheduleId { get; set; }
        public string Subject { get; set; } = string.Empty;
        public int Recipients { get; set; }
        public string ScheduledTime { get; set; } = string.Empty;
        public EmailStatus Status { get; set; }
    }
}