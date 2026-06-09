using EmailSchedulerApp.Enums;

public class EmailSchedule
{
    public int ScheduleId { get; set; }

    public string Subject { get; set; } = string.Empty;

    public string Body { get; set; } = string.Empty;

    public string? Recipients { get; set; }
    
    public DateTime ScheduledTime { get; set; }

    public EmailStatus Status { get; set; }

    public int RetryCount { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }
}