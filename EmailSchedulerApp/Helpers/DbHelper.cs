using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EmailSchedulerApp.Helpers
{
    public class DbHelper(IConfiguration config)
    {
        private readonly IConfiguration _config = config;

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(
                _config.GetConnectionString("DefaultConnection")
            );
        }
    }
}