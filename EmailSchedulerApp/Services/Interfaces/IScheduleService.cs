namespace EmailSchedulerApp.Services.Interfaces
{
    public interface IScheduleService
    {
        Task<DashboardDto> GetDashboardDataAsync();
    }
}