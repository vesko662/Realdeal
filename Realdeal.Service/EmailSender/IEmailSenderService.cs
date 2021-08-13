using Realdeal.Service.EmailSender.Model;

namespace Realdeal.Service.EmailSender
{
    public interface IEmailSenderService
    {
        void SendEmail(Message message);
    }
}
