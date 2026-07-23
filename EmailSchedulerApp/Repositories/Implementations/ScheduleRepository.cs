using Dapper;
using EmailSchedulerApp.DTOs.Dashboard;
using EmailSchedulerApp.DTOs.Template;
using EmailSchedulerApp.Helpers;
using EmailSchedulerApp.Models;
using EmailSchedulerApp.Repositories.Interfaces;
using System.Data;

namespace EmailSchedulerApp.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly DbHelper _db;

        public ScheduleRepository(DbHelper db)
        {
            _db = db;
        }

        public async Task<int> SaveSchedule(Schedule schedule)
        {
            using IDbConnection conn = _db.CreateConnection();
            string query = @" INSERT INTO Schedules ( TemplateId, StartDate, EndDate, IsActive, CreatedOn, Name, Channel, Description, StartTime, Timezone, Frequency, WeekDays, CronExpression, Tags, Priority, UpdatedOn, CreatedBy ) VALUES ( @TemplateId, @StartDate, @EndDate, @IsActive, @CreatedOn, @Name, @Channel, @Description, @StartTime, @Timezone, @Frequency, @WeekDays, @CronExpression, @Tags, @Priority, @UpdatedOn, @CreatedBy ); SELECT CAST(SCOPE_IDENTITY() AS INT);";
            return await conn.ExecuteScalarAsync<int>(query, schedule);
        }

        public async Task SaveRecipients(List<Recipient> recipients)
        {
            using IDbConnection conn = _db.CreateConnection();
            string query = @" INSERT INTO Recipients ( ScheduleId, Name, Email, Source, AddedAt ) VALUES ( @ScheduleId, @Name, @Email, @Source, @AddedAt );";
            await conn.ExecuteAsync(query, recipients);
        }

        public async Task<List<TemplateDropdownDto>> GetTemplatesAsync()
        {
            using var connection = _db.CreateConnection();
            string query = @" SELECT TemplateId, TemplateName FROM EmailTemplate WHERE IsActive = 1 ORDER BY TemplateName;";
            var result = await connection.QueryAsync<TemplateDropdownDto>(query);
            return result.ToList();
        }
    }
}