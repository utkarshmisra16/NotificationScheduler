namespace EmailSchedulerApp.DTOs.Dashboard
{
    public class RecentEmailScheduleDto
    {
        public int ScheduleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Recipients { get; set; } = string.Empty;
        public int RecipientCount { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public bool IsActive { get; set; }
    }
}