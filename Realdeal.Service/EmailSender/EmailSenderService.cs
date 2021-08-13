using MimeKit;
using Realdeal.Service.EmailSender.Configuration;
using Realdeal.Service.EmailSender.Model;
using MimeKit.Text;
using MailKit.Net.Smtp;

namespace Realdeal.Service.EmailSender
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly EmailConfiguration emailConfig;

        public EmailSenderService(EmailConfiguration emailConfiguration)
        {
            this.emailConfig = emailConfiguration;
        }
        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage=new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(TextFormat.Text) { Text = message.Content };

            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client=new SmtpClient())
            {
                try
                {
                    client.Connect(emailConfig.SmtpServer, emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(emailConfig.UserName, emailConfig.Password);

                    client.Send(mailMessage);
                }
                catch (System.Exception)
                {

                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
