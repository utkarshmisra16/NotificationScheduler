using Dapper;
using EmailSchedulerApp.Data;
using System.Data;
using System.Data.SqlClient;

public class DashboardRepository(DbConnectionFactory factory) : IDashboardRepository
{
    private readonly DbConnectionFactory _factory = factory;

    public Task<DashboardDto> GetDashboardData()
    {
        throw new NotImplementedException();
    }

    public async Task<DashboardDto> GetDashboardDataAsync()
    {
        using var connection = _factory.CreateConnection();

        var query = @"
        -- Dashboard Summary
        SELECT
        COUNT(ScheduleId) AS TotalEmails,
        SUM(CASE WHEN Status = 0 THEN 1 ELSE 0 END) AS Pending,
        SUM(CASE WHEN Status = 1 THEN 1 ELSE 0 END) AS Sent,
        SUM(CASE WHEN Status = 2 THEN 1 ELSE 0 END) AS Failed,
        SUM(CASE WHEN ScheduledTime < GETDATE() AND Status = 0 THEN 1 ELSE 0 END) AS Overdue
        FROM EmailSchedule;

        -- Recent Emails 
        SELECT TOP 10 ScheduleId, Subject, Body, ScheduledTime, Status, RetryCount, CreatedBy, CreatedOn, UpdatedOn FROM EmailSchedule ORDER BY ScheduleId DESC; ";

        using var multi = await connection.QueryMultipleAsync(query);

        // First result → counts
        var dashboard = await multi.ReadFirstAsync<DashboardDto>();

        // Second result → list
        dashboard.RecentEmails = (await multi.ReadAsync<EmailSchedule>()).ToList();

        return dashboard;
    }
}