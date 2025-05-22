using IDENTITY.BLL.Configurations;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using MimeKit;
using System.Text;

namespace IDENTITY.BLL.Services
{

    public class EmailSender : IEmailSender
    {
        public EmailSender(ILogger<EmailSender> logger, EmailSenderConfiguration configuration)
        {
            Logger = logger;
            Configuration = configuration;
        }

        public ILogger<EmailSender> Logger { get; }
        public EmailSenderConfiguration Configuration { get; }

        public async Task SendEmailAsync(string recipientEmail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(Configuration.Name, Configuration.Adress));
            message.To.Add(new MailboxAddress("", recipientEmail));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = body;

            message.Body = bodyBuilder.ToMessageBody();

            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(Configuration.Host, Configuration.Port, Configuration.UseSsl);
                    await client.AuthenticateAsync(Configuration.Adress, Configuration.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                Logger.LogInformation($"Email sent to {recipientEmail}: {subject}");
            }
            catch (AuthenticationException ex)
            {
                Logger.LogError($"Authentication failed when sending email: {ex.Message}");
                throw; // або кинути кастомну помилку
            }
            catch (Exception ex)
            {
                Logger.LogError($"Unexpected error during email sending: {ex.Message}");
                throw;
            }

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("Email Confirmation Message");
            stringBuilder.AppendLine("--------------------------");
            stringBuilder.AppendLine($"TO: {recipientEmail}");
            stringBuilder.AppendLine($"SUBJECT: {subject}");
            stringBuilder.AppendLine($"CONTENTS: {body}");
            stringBuilder.AppendLine();
            Logger.Log(LogLevel.Information, stringBuilder.ToString());
        }
    }
}