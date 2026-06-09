using EmailSchedulerApp.DTOs.Template;
namespace EmailSchedulerApp.Services.Interfaces
{
    public interface ITemplateService
    {
        Task<CreateTemplateRequestDto> GetDashboardDataAsync();
    }
}