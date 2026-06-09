namespace EmailSchedulerApp.DTOs.Template
{
    public class CreateTemplateResponseDto
    {
        public int TemplateId {get; set;}
        public string Message {get; set;} = string.Empty;
        public bool Success {get; set;}
    }
}