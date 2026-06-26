using Dapper;
using EmailSchedulerApp.Helpers;
using EmailSchedulerApp.Repositories.Interfaces;
namespace EmailSchedulerApp.Repositories.Implementations
{
    public class TemplateRepository(DbHelper dbHelper) : ITemplateRepository
    {
        private readonly DbHelper _dbHelper = dbHelper;
        public async Task<bool> SaveTemplate(Models.Template template)
        {
            string query = "Insert into EmailTemplate (TemplateName, Subject, Body, IsActive, CreatedOn) values (@TemplateName, @Subject, @Body, @IsActive, @CreatedOn)";
            using var connection = _dbHelper.CreateConnection();
            int rowsAffected = await connection.ExecuteAsync(query, template);
            return rowsAffected > 0;
        }
    }
}
