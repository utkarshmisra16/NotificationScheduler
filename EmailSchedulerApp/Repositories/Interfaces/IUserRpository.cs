using EmailSchedulerApp.DTOs;
using EmailSchedulerApp.Models;
namespace EmailSchedulerApp.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User? GetUserByUsername(string? Email);
        void InvalidatePasswordResetTokens(int userId);
        void CreatePasswordResetToken(PasswordResetToken passwordResetToken);
        PasswordResetToken? GetPasswordResetToken(string tokenHash);
        void MarkPasswordResetTokenAsUsed(int tokenId);
        void UpdatePassword(int userId, string passwordHash);
        void Register(RegisterRequestDto request);
    }
}