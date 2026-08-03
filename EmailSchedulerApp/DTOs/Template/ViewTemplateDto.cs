namespace EmailSchedulerApp.DTOs.Template
{
    public class ViewTemplateDto
    {
        public int TemplateId { get; set; }
        public string TemplateName { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }
    }
}