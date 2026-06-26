namespace EmailSchedulerApp.DTOs.Dashboard
{
    public class RecentEmailScheduleDto
    {
        public int ScheduleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int RecipientCount { get; set; }
        public DateOnly StartDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public bool IsActive { get; set; }
    }
}