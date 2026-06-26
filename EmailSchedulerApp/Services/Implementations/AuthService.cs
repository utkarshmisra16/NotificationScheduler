using EmailSchedulerApp.DTOs;
using EmailSchedulerApp.Models;
using EmailSchedulerApp.Repositories.Interfaces;
using EmailSchedulerApp.Services.Interfaces;
using BCrypt.Net;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public LoginResponsedto Login(LoginRequestDto request)
    {
        User? user = _userRepository.GetUserByUsername(request.Email);

        if (user == null)
        {
            return new LoginResponsedto
            {
                Success = false,
                Message = "User not found"
            };
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
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