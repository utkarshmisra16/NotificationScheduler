using EmailSchedulerApp.DTOs;
using EmailSchedulerApp.Models;
using EmailSchedulerApp.Repositories.Interfaces;
using EmailSchedulerApp.Services.Interfaces;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public LoginResponsedto Login(LoginRequestDto request)
    {
        User user = _userRepository.GetUserByUsername(request.Username);

        if (user == null)
        {
            return new LoginResponsedto
            {
                Success = false,
                Message = "User not found"
            };
        }

        if (user.PasswordHash != request.Password)
        {
            return new LoginResponsedto
            {
                Success = false,
                Message = "Invalid password"
            };
        }

        return new LoginResponsedto
        {
            Success = true,
            Message = "Login successful"
        };
    }
}