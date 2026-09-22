using EmailSchedulerApp.Models;

namespace EmailSchedulerApp.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessage message);
    }
}