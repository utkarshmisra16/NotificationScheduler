using EmailSchedulerApp.DTOs.Template;
namespace EmailSchedulerApp.Services.Interfaces
{
    public interface ITemplateService
    {
        Task<CreateTemplateResponseDto> SaveTemplate(CreateTemplateRequestDto request);
        Task<TemplatePaginationDto> GetTemplatesAsync(int page, int pageSize);
    }
}