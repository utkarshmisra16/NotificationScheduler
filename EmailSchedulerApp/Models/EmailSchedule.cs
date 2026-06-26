using System;
using EmailSchedulerApp.Enums;

namespace EmailSchedulerApp.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public int TemplateId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Channel { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TimeSpan StartTime { get; set; }
        public string Timezone { get; set; } = string.Empty;
        public int Frequency { get; set; }
        public string? WeekDays { get; set; }
        public string? CronExpression { get; set; }
        public string? Tags { get; set; }
        public int Priority { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }
}