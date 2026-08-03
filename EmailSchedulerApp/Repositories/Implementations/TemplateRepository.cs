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

        public async Task<List<ViewTemplateDto>> GetTemplatesAsync( int page, int pageSize)
        {
            const string query = @"
                SELECT TemplateId, TemplateName, Subject, CreatedBy, CreatedOn, IsActive FROM EmailTemplate ORDER BY CreatedOn DESC OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";
            int offset = (page - 1) * pageSize;
            using var connection = _dbHelper.CreateConnection();
            var result = await connection.QueryAsync<ViewTemplateDto>(
                query,new{ Offset = offset, PageSize = pageSize });
            return result.ToList();
        }

        public async Task<int> GetTemplatesCountAsync()
        {
            const string query = "SELECT COUNT(TemplateId) FROM EmailTemplate";
            using var connection = _dbHelper.CreateConnection();
            int count = await connection.ExecuteScalarAsync<int>(query);
            return count;
        }
    }
}
