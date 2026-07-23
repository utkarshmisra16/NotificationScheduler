using EmailSchedulerApp.DTOs.Template;
using EmailSchedulerApp.Models;

namespace EmailSchedulerApp.Repositories.Interfaces
{
    public interface IScheduleRepository
    {
        Task<int> SaveSchedule(Schedule schedule);
        Task SaveRecipients(List<Recipient> recipients);
        Task<List<TemplateDropdownDto>> GetTemplatesAsync();
    }
}