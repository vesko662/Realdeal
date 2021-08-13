using Realdeal.Data;
using Realdeal.Data.Models;
using Realdeal.Models.Advert;
using Realdeal.Service.User;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Realdeal.Service.Observe
{
    public class ObserveService : IObserveService
    {
        private readonly RealdealDbContext context;
        private readonly IUserService userService;

        public ObserveService(RealdealDbContext context, IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }
        public IEnumerable<AdvertShowingViewModel> GetAllObservingAdverts()
        => context.ObservedAdverts
            .Where(x => x.UserId == userService.GetCurrentUserId())
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
            throw new NotImplementedException();
        }

        public bool StartObservingAdvert(string advertId)
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
            };

            advert.ОbservedAdverts.Add(observe);

            context.SaveChanges();

            return true;
        }

        public bool StopObservingAdvert(string advertId)
        {
            throw new NotImplementedException();
        }
    }
}
