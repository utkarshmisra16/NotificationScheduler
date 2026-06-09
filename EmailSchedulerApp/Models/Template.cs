namespace EmailSchedulerApp.Models
{
    public class Template
    {
        public int TemplateId { get; set; }

        public string TemplateName { get; set; } = string.Empty;

        public string Subject { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? CreatedBy { get; set; }
    }
}