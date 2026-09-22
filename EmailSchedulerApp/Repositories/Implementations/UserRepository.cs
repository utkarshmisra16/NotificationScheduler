using Dapper;
using EmailSchedulerApp.DTOs;
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
            string query = "SELECT UserId, Email, PasswordHash, FullName, IsActive, CreatedAt FROM Users WHERE Email = @Email;";
            return conn.QueryFirstOrDefault<User>(query, new { Email });
        }

        public void InvalidatePasswordResetTokens(int userId)
        {
            using IDbConnection conn = _db.CreateConnection();
            string query = "UPDATE PasswordResetToken SET IsUsed = 1 WHERE UserId = @UserId AND IsUsed = 0;";
            conn.Execute(query, new { UserId = userId });
        }

        public void CreatePasswordResetToken(PasswordResetToken passwordResetToken)
        {
            using IDbConnection conn = _db.CreateConnection();
            string query = "INSERT INTO PasswordResetToken (UserId, TokenHash, ExpiresAt, IsUsed, CreatedAt) VALUES ( @UserId, @TokenHash, @ExpiresAt, @IsUsed, @CreatedAt );"; 
            conn.Execute(query, passwordResetToken);
        }

        public PasswordResetToken? GetPasswordResetToken(string tokenHash)
        {
            using IDbConnection conn = _db.CreateConnection();
            string query = "SELECT Id, UserId, TokenHash, ExpiresAt, IsUsed, CreatedAt FROM PasswordResetToken WHERE TokenHash = @TokenHash;";
            return conn.QueryFirstOrDefault<PasswordResetToken>(query, new { TokenHash = tokenHash });
        }

        public void MarkPasswordResetTokenAsUsed(int tokenId)
        {
            using IDbConnection conn = _db.CreateConnection();
            string query = "UPDATE PasswordResetToken SET IsUsed = 1 WHERE Id = @Id;";
            conn.Execute(query, new { Id = tokenId });
        }

        public void UpdatePassword(int userId, string passwordHash)
        {
            using IDbConnection conn = _db.CreateConnection();
            string query = "UPDATE Users SET PasswordHash = @PasswordHash WHERE UserId = @UserId;";
            conn.Execute(query, new { UserId = userId, PasswordHash = passwordHash });
        }

        public void Register(RegisterRequestDto request)
        {
            using IDbConnection conn = _db.CreateConnection();
            string query = "INSERT INTO Users (Email, PasswordHash, FullName, IsActive, CreatedAt) VALUES (@Email, @PasswordHash, @FullName, @IsActive, @CreatedAt);";
            var parameters = new
            {
                request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                request.FullName,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            conn.Execute(query, parameters);
        }
    } 
}