using System.ComponentModel.DataAnnotations;

namespace EmailSchedulerApp.DTOs.Template
{
    public class CreateTemplateRequestDto
    {
        [Required(ErrorMessage = "Template Name is required.")]
        [StringLength(100, ErrorMessage = "Template Name cannot exceed 100 characters.")]
        public string TemplateName {get; set;} = string.Empty;

        [Required(ErrorMessage = "Subject is required.")]
        [StringLength(500, ErrorMessage = "Subject cannot exceed 500 characters.")]
        public string Subject {get; set;} = string.Empty;

        [Required(ErrorMessage = "Body is required.")]
        public string Body {get; set;} = string.Empty;
        public List<IFormFile>? Attachments { get; set; }
        public bool IsActive {get; set;} = true;
    }
}