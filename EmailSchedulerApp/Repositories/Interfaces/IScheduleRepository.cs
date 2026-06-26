using EmailSchedulerApp.DTOs.Dashboard;
using EmailSchedulerApp.Models;

namespace EmailSchedulerApp.Repositories.Interfaces
{
    public interface IScheduleRepository
    {
        Task<int> SaveSchedule(Schedule schedule);

        Task SaveRecipients(List<Recipient> recipients);
    }
}