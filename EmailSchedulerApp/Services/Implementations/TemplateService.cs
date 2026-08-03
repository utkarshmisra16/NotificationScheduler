using EmailSchedulerApp.DTOs.Template;
using EmailSchedulerApp.Models;
using EmailSchedulerApp.Repositories.Interfaces;
using EmailSchedulerApp.Services.Interfaces;

namespace EmailSchedulerApp.Services.Implementations
{
    public class TemplateService(ITemplateRepository templateRepository) : ITemplateService
    {
        private readonly ITemplateRepository _repository = templateRepository;

        public async Task<CreateTemplateResponseDto> SaveTemplate(CreateTemplateRequestDto request)
        {
            Template template = new()
            {
                TemplateName = request.TemplateName,
                Subject = request.Subject,
                Body = request.Body,
                IsActive = request.IsActive,
                CreatedOn = DateTime.Now
            };
            bool isSaved = await _repository.SaveTemplate(template);

            return new CreateTemplateResponseDto
            {
                Success = isSaved,
                Message = isSaved ? "Template saved successfully." : "Failed to save template."
            };
        }

        public async Task<TemplatePaginationDto> GetTemplatesAsync(int page, int pageSize)
        {
            var templates = await _repository.GetTemplatesAsync(page, pageSize);
            int totalRecords = await _repository.GetTemplatesCountAsync();
            return new TemplatePaginationDto
            {
                Page = page,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                Templates = templates
            };
        }
    }
}