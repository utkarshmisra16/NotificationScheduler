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
            var connectionString = _config.GetConnectionString("DefaultConnection");

            var connection = new SqlConnection(connectionString);

            connection.Open();   

            return connection;
        }
    }                
}