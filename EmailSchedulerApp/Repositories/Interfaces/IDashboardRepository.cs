using EmailSchedulerApp.DTOs.Dashboard;
namespace EmailSchedulerApp.Repositories.Interfaces
{
    public interface IDashboardRepository
    {
        // Task<DashboardDto> GetDashboardDataAsync();
        Task<List<RecentEmailScheduleDto>> GetRecentSchedulesAsync();
    }
}