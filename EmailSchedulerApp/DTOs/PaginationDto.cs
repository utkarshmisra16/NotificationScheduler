namespace EmailSchedulerApp.DTOs.Template
{
    public class TemplatePaginationDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }

        public List<ViewTemplateDto> Templates { get; set; } = new();
    }
}