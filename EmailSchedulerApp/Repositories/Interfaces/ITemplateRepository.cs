using EmailSchedulerApp.DTOs.Template;
using EmailSchedulerApp.Models;
namespace EmailSchedulerApp.Repositories.Interfaces
{
    public interface ITemplateRepository
    {
        Task<bool> SaveTemplate(Template template);
        Task<List<ViewTemplateDto>> GetTemplatesAsync(int page, int pageSize);
        Task<int> GetTemplatesCountAsync();
    }
}