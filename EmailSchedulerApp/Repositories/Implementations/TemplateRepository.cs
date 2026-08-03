using Dapper;
using EmailSchedulerApp.DTOs.Template;
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

        public List<ViewTemplateDto> GetTemplates()
        {
            string query = "Select TemplateName, Subject, Body, IsActive, CreatedOn from EmailTemplate";
            using var connection = _dbHelper.CreateConnection();
            return connection.Query<ViewTemplateDto>(query).ToList();
        }
    }
}
