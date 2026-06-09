using EmailSchedulerApp.Services.Interfaces;

namespace EmailSchedulerApp.Services.Implementation
{
    public class DashboardService(IDashboardRepository repository) : IDashboardService
    {
        private readonly IDashboardRepository _repository = repository;

        public async Task<DashboardDto> GetDashboardDataAsync()
        {
            return await _repository.GetDashboardDataAsync();
        }
    }
}