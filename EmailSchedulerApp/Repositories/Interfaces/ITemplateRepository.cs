using EmailSchedulerApp.Models;
namespace EmailSchedulerApp.Repositories.Interfaces
{
    public interface ITemplateRepository
    {
        Task<bool> SaveTemplate(Template template);
    }
}