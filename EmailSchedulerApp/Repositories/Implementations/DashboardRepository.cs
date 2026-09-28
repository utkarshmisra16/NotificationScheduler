using Dapper;
using EmailSchedulerApp.DTOs.Dashboard;
using EmailSchedulerApp.Helpers;
using EmailSchedulerApp.Repositories.Interfaces;

namespace EmailSchedulerApp.Repositories.Implementations
{
    public class DashboardRepository(DbHelper db) : IDashboardRepository
    {
        private readonly DbHelper _db = db;

        public async Task<List<RecentEmailScheduleDto>> GetRecentSchedulesAsync()
        {
            using var conn = _db.CreateConnection();
            string query = "SELECT TOP 10 S.ScheduleId, S.Name, S.Recipients, ( SELECT COUNT(*) FROM OPENJSON(S.Recipients) ) AS RecipientCount, S.StartDate, S.StartTime, S.IsActive FROM Schedules S ORDER BY S.CreatedOn DESC; ";
            var result = await conn.QueryAsync<RecentEmailScheduleDto>(query);
            return result.ToList();
        }
    }
}