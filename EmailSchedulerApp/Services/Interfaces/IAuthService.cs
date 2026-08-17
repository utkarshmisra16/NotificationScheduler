using EmailSchedulerApp.DTOs;

namespace EmailSchedulerApp.Services.Interfaces
{
    public interface IAuthService
    {
        LoginResponsedto Login(LoginRequestDto request);
        // bool ForgotPassword(string email);
    }
}