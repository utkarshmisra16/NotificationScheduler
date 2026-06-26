using System.ComponentModel.DataAnnotations;
using EmailSchedulerApp.Enums;
using System.Collections.Generic;
 
namespace EmailSchedulerApp.DTOs.Schedule
{
    public class CreateScheduleRequestDto
    {
 
        [Required(ErrorMessage = "Schedule name is required.")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string? Name { get; set; }
 
        [Required(ErrorMessage = "Notification channel is required.")]
        public string Channel { get; set; } = "Email";
 
        [Required(ErrorMessage = "Please select a template.")]
        public int TemplateId { get; set; }
 
        [MaxLength(500)]
        public string? Description { get; set; }
 
        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }
 
        [Required(ErrorMessage = "Start time is required.")]
        public TimeSpan StartTime { get; set; }
 
        [Required(ErrorMessage = "Timezone is required.")]
        public string Timezone { get; set; } = "IST";
 
        [Required(ErrorMessage = "Frequency is required.")]
        public FrequencyType Frequency { get; set; }

        public List<string>? WeekDays { get; set; }
 
        public string? CronExpression { get; set; }
 
        public DateTime? EndDate { get; set; }
 
        [Required(ErrorMessage = "At least one recipient email is required.")]
        [MinLength(1, ErrorMessage = "At least one recipient email is required.")]
        public List<string> RecipientEmails { get; set; } = new();
 
        public string? Tags { get; set; }
 
        public PriorityLevel Priority { get; set; } = PriorityLevel.Medium;
    }
}