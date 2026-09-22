using EmailSchedulerApp.DTOs;

namespace EmailSchedulerApp.Services.Interfaces
{
    public interface IAuthService
    {
        LoginResponsedto Login(LoginRequestDto request);
        LoginResponsedto Register(RegisterRequestDto request);
        Task<bool> ForgotPassword(string email, string baseUrl);
        bool ResetPassword(string token, string newPassword);
    }
}