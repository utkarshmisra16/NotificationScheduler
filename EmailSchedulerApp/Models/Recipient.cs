using System;

namespace EmailSchedulerApp.Models
{
    public class Recipient
    {
        public int RecipientId { get; set; }

        public int ScheduleId { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Source { get; set; }

        public DateTime AddedAt { get; set; }
    }
}