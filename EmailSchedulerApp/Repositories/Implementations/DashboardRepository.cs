using Dapper;
using EmailSchedulerApp.DTOs.Dashboard;
using EmailSchedulerApp.Helpers;
using EmailSchedulerApp.Repositories.Interfaces;

namespace EmailSchedulerApp.Repositories.Implementations
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly DbHelper _db;

        public DashboardRepository(DbHelper db)
        {
            _db = db;
        }
        
        public async Task<List<RecentEmailScheduleDto>> GetRecentSchedulesAsync()
        {
            using var conn = _db.CreateConnection();
            string query = @" SELECT TOP 10 S.ScheduleId, S.Name, COUNT(R.RecipientId) AS RecipientCount, S.StartDate, S.StartTime, S.IsActive FROM Schedules S LEFT JOIN Recipients R ON S.ScheduleId = R.ScheduleId GROUP BY S.ScheduleId, S.Name, S.StartDate, S.StartTime, S.IsActive, S.CreatedOn ORDER BY S.CreatedOn DESC;";
            var result = await conn.QueryAsync<RecentEmailScheduleDto>(query);
            return result.ToList();
        }
    }
}