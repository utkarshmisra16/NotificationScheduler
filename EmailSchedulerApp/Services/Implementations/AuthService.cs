using System.Security.Cryptography;
using System.Text;
using EmailSchedulerApp.DTOs;
using EmailSchedulerApp.Models;
using EmailSchedulerApp.Repositories.Interfaces;
using EmailSchedulerApp.Services.Interfaces;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public AuthService(IUserRepository userRepository, IEmailService emailService)
    {
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public LoginResponsedto Login(LoginRequestDto request)
    {
        User? user = _userRepository.GetUserByUsername(request.Email);
        if (user == null)
            return new LoginResponsedto { Success = false, Message = "User not found" };

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return new LoginResponsedto { Success = false, Message = "Invalid password" };

        return new LoginResponsedto { Success = true, Message = "Login successful" };
    }

    public LoginResponsedto Register(RegisterRequestDto request)
    {
        _userRepository.Register(request);
        return new LoginResponsedto { Success = true, Message = "User registered successfully" };
    }

    public async Task<bool> ForgotPassword(string email, string baseUrl)
    {
        User? user = _userRepository.GetUserByUsername(email);
        if (user == null || !user.IsActive)
            return false;
        // Generate secure random token
        byte[] tokenBytes = RandomNumberGenerator.GetBytes(32);
        string token = Convert.ToBase64String(tokenBytes);
        // Hash token before storing it in database
        string tokenHash = GenerateTokenHash(token);
        // Invalidate previous unused tokens
        _userRepository.InvalidatePasswordResetTokens(user.UserId);
        PasswordResetToken passwordResetToken = new()
        {
            UserId = user.UserId,
            TokenHash = tokenHash,
            ExpiresAt = DateTime.UtcNow.AddMinutes(30),
            IsUsed = false,
            CreatedAt = DateTime.UtcNow
        };
        _userRepository.CreatePasswordResetToken(passwordResetToken);

        // Email sending will be added here\
        // Reset password link
        string resetLink = $"{baseUrl}/Auth/Auth/ResetPassword?token={Uri.EscapeDataString(token)}";
        string emailBody = $@"
        <html>
        <body>
            <h2>Reset Your Password</h2>

            <p>Hello {user.FullName},</p>

            <p>
                We received a request to reset your password.
            </p>

            <p>
                Click the button below to reset your password:
            </p>

            <p>
                <a href=""{resetLink}""
                   style=""
                       display:inline-block;
                       padding:10px 20px;
                       background-color:#007bff;
                       color:white;
                       text-decoration:none;
                       border-radius:5px;
                   "">
                    Reset Password
                </a>
            </p>

            <p>
                This link will expire in <strong>30 minutes</strong>.
            </p>

            <p>
                If you did not request a password reset,
                you can safely ignore this email.
            </p>

            <p>
                Regards,<br/>
                Email Scheduler Team
            </p>
        </body>
        </html>";

        await _emailService.SendEmailAsync(new EmailMessage
        {
            To = user.Email ?? string.Empty,
            Subject = "Reset Your Password",
            Body = emailBody,
            IsBodyHtml = true
        });

        return true;
    }

    public bool ResetPassword(string token, string newPassword)
    {
        string tokenHash = GenerateTokenHash(token);
        PasswordResetToken? resetToken = _userRepository.GetPasswordResetToken(tokenHash);

        if (resetToken == null)
            return false;

        if (resetToken.IsUsed)
            return false;

        if (resetToken.ExpiresAt <= DateTime.UtcNow)
            return false;

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
        _userRepository.UpdatePassword(resetToken.UserId, passwordHash);
        _userRepository.MarkPasswordResetTokenAsUsed(resetToken.Id);
        return true;
    }

    private static string GenerateTokenHash(string token)
    {
        byte[] hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(token));
        return Convert.ToHexString(hashBytes);
    }
}