using EmailSchedulerApp.Enums;
using EmailSchedulerApp.Repositories.Interfaces;
using EmailSchedulerApp.Services.Interfaces;
using EmailSchedulerApp.ViewModels.Dashboard;

namespace EmailSchedulerApp.Services.Implementation
{
    public class DashboardService(IDashboardRepository repository) : IDashboardService
    {
        private readonly IDashboardRepository _repository = repository;

        // public async Task<DashboardDto> GetDashboardDataAsync()
        // {
        //     return await _repository.GetDashboardDataAsync();
        // }

        public async Task<DashboardViewModel> GetDashboardAsync()
        {
            var recentSchedules = await _repository.GetRecentSchedulesAsync();
            return new DashboardViewModel
            {
                RecentEmails = recentSchedules
                    .Select(x => new RecentEmailViewModel
                    {
                        ScheduleId = x.ScheduleId,
                        Subject = x.Name,
                        Recipients = x.RecipientCount,
                        ScheduledTime = $"{x.StartDate:dd MMM yyyy} {x.StartTime}",
                        Status = x.IsActive ? EmailStatus.Pending : EmailStatus.Sent
                    })
                    .ToList()
            };
        }
    }
}