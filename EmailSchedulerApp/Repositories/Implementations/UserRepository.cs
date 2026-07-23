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

            string query = @" SELECT UserId, Email, PasswordHash, FullName, IsActive, CreatedAt FROM Users WHERE Email = @Email;";

            return conn.QueryFirstOrDefault<User>(query, new { Email });
        }
    }
}