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

        public User? GetUserByUsername(string? Email)
        {
            using IDbConnection conn = _db.CreateConnection();

            string query = @"SELECT u.UserId, am.PasswordHash FROM Users u JOIN UserAuthMethods am ON u.UserId = am.UserId
                            WHERE u.Email = @Email AND am.Provider = 'EMAIL';";

            return conn.QueryFirstOrDefault<User>(query, new { Email });
        }
    }
}