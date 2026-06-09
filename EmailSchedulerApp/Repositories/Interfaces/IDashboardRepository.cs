public interface IDashboardRepository
{
    Task<DashboardDto> GetDashboardDataAsync();
}