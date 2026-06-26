using EmailSchedulerApp.ViewModels.Dashboard;

namespace EmailSchedulerApp.Services.Interfaces
{
    public interface IDashboardService
    {
        // Task<DashboardDto> GetDashboardDataAsync();
        Task<DashboardViewModel> GetDashboardAsync();
    }
}