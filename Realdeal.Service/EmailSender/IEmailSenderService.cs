using Realdeal.Service.EmailSender;

namespace Realdeal.Service.EmailSender
{
    public interface IEmailSenderService
    {
        void SendEmail(Model.Message message);
    }
}
