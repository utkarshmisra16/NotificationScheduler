using System.Net;
using System.Net.Mail;
using EmailSchedulerApp.Models;
using EmailSchedulerApp.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace EmailSchedulerApp.Services.Implementations
{
    public class EmailService(IOptions<EmailSettings> emailSettings) : IEmailService
    {
        private readonly EmailSettings _emailSettings = emailSettings.Value;

        public async Task SendEmailAsync(EmailMessage message)
        {
            using MailMessage mailMessage = new();
            mailMessage.From = new MailAddress(
                _emailSettings.SenderEmail,
                _emailSettings.SenderName
            );
            mailMessage.To.Add(message.To);
            if (!string.IsNullOrWhiteSpace(message.Cc))
                mailMessage.CC.Add(message.Cc);

            if (!string.IsNullOrWhiteSpace(message.Bcc))
                mailMessage.Bcc.Add(message.Bcc);

            mailMessage.Subject = message.Subject;
            mailMessage.Body = message.Body;
            mailMessage.IsBodyHtml = message.IsBodyHtml;

            using SmtpClient smtpClient = new SmtpClient(
                _emailSettings.SmtpServer,
                _emailSettings.Port
            );

            smtpClient.Credentials = new NetworkCredential(
                _emailSettings.Username,
                _emailSettings.Password
            );

            smtpClient.EnableSsl = _emailSettings.EnableSsl;
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}