using Dapper;
using EmailSchedulerApp.Helpers;
using EmailSchedulerApp.Models;
using EmailSchedulerApp.Repositories.Interfaces;
using System.Data;

namespace EmailSchedulerApp.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly DbHelper _db;

        public UserRepository(DbHelper db)
        {
            _db = db;
        }

        public User GetUserByUsername(string username)
        {
            using IDbConnection conn = _db.CreateConnection();

            string query = @"
                SELECT TOP 1 *
                FROM Users
                WHERE Username = @Username AND IsActive = 1";

            return conn.QueryFirstOrDefault<User>(query, new { Username = username });
        }
    }
}