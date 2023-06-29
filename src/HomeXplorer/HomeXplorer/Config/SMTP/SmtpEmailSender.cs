namespace HomeXplorer.Config.SMTP
{
    using System.Threading.Tasks;

    using Microsoft.Extensions.Options;
    using Microsoft.AspNetCore.Identity.UI.Services;

    using MimeKit;
    using MailKit.Net.Smtp;
    using MailKit.Security;

    using static SmtpConstants;

    public class SmtpEmailSender
        : IEmailSender
    {
        private readonly SmtpSettings smtpSettings;

        public SmtpEmailSender(IOptions<SmtpSettings> smtpSettings)
        {
            this.smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            //add check for email and message

            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(EmailSenderName, smtpSettings.UserName));
            emailMessage.To.Add(new MailboxAddress("", email)); 
            emailMessage.Subject = subject;

            emailMessage.Body = new TextPart("html")
            {
                Text = message //message to be a constant
            };

            using var client = new SmtpClient();

            await client.ConnectAsync(smtpSettings.Host, smtpSettings.Port,
                SecureSocketOptions.StartTls);

            await client.AuthenticateAsync(smtpSettings.UserName, smtpSettings.Password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
