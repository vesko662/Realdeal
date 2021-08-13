using Realdeal.Data;
using Realdeal.Data.Models;
using Realdeal.Models.Advert;
using Realdeal.Service.EmailSender;
using Realdeal.Service.User;
using System.Collections.Generic;
using System.Linq;

namespace Realdeal.Service.Observe
{
    public class ObserveService : IObserveService
    {
        private readonly RealdealDbContext context;
        private readonly IUserService userService;
        private readonly IEmailSenderService emailSender;

        public ObserveService(RealdealDbContext context,
            IUserService userService,
            IEmailSenderService emailSender)
        {
            this.context = context;
            this.userService = userService;
            this.emailSender = emailSender;
        }
        public IEnumerable<AdvertShowingViewModel> GetAllObservingAdverts()
        => context.ObservedAdverts
            .Where(x => x.UserId == userService.GetCurrentUserId())
            .Where(x => x.Advert.IsАrchived == false && x.Advert.IsDeleted == false)
            .Select(s => new AdvertShowingViewModel()
            {
                Id = s.AdvertId,
                Description = s.Advert.Description,
                ImageURL = s.Advert.AdvertImages.FirstOrDefault().ImageUrl,
                Name = s.Advert.Name,
                Price = s.Advert.Price,
            })
            .ToList();

        public bool IsAdvertObserved(string advertId)
        {
            var observedAdvert = context.ObservedAdverts
                .Where(x => x.AdvertId == advertId && x.UserId == userService.GetCurrentUserId())
                .FirstOrDefault();

            return observedAdvert == null ? false : true;
        }
        public void RemoveAllObservingUsers(string advertId)
        {
            var observedAdverts = context.ObservedAdverts.Where(x => x.AdvertId == advertId).ToList();

            if (observedAdverts == null)
                return;

            context.ObservedAdverts.RemoveRange(observedAdverts);

            context.SaveChanges();
        }

        public void SendEmailOUpdate(string advertId, string title, string content)
        {
            var advertName = context.Adverts.Find(advertId).Name;

            content = string.Format(content, advertName);

            var usersEmail = GetAllObservingUsersEmail(advertId);

            var emailMessage = new EmailSender.Model.Message(usersEmail, title, content);

            emailSender.SendEmail(emailMessage);
        }

        public bool StartObservingAdvert(string advertId, bool emailNothification)
        {
            var advert = context.Adverts
                   .Where(x => x.Id == advertId)
                   .Where(x => x.IsАrchived == false && x.IsDeleted == false)
                   .FirstOrDefault();

            if (advert == null)
            {
                return false;
            }

            var observe = new ОbservedAdvert()
            {
                AdvertId = advertId,
                UserId = userService.GetCurrentUserId(),
                SendEmailOnUpdate = emailNothification,
            };

            advert.ОbservedAdverts.Add(observe);

            context.SaveChanges();

            return true;
        }

        public bool StopObservingAdvert(string advertId)
        {
            var observerdAdvert = context.ObservedAdverts
                 .Where(x => x.AdvertId == advertId && x.UserId == userService.GetCurrentUserId())
                 .FirstOrDefault();

            if (observerdAdvert == null)
            {
                return false;
            }

            context.ObservedAdverts.Remove(observerdAdvert);

            context.SaveChanges();

            return true;
        }

        private IEnumerable<string> GetAllObservingUsersEmail(string advertId)
        => context.ObservedAdverts
            .Where(x => x.AdvertId == advertId && x.SendEmailOnUpdate == true)
            .Select(s => s.User.Email)
            .ToList();
    }
}
