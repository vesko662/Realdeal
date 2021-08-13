using Realdeal.Data;
using static Realdeal.Common.GlobalConstants;
using Realdeal.Service.User;
using System.Linq;
using System.Collections.Generic;
using Realdeal.Models.Advert;
using System;
using Realdeal.Models.Archive;
using Microsoft.EntityFrameworkCore;
using Realdeal.Service.Observe;

namespace Realdeal.Service.Archive
{
    public class ArchiveService : IArchiveService
    {
        private readonly RealdealDbContext context;
        private readonly IUserService userService;
        private readonly IObserveService observeService;

        public ArchiveService(RealdealDbContext context,
            IUserService userService,
            IObserveService observeService)
        {
            this.context = context;
            this.userService = userService;
            this.observeService = observeService;
        }

        public bool AddAdvertToArchive(string advertId)
        {
            var advert = context.Adverts
                   .Where(x => x.Id == advertId && x.UserId == userService.GetCurrentUserId())
                   .Where(x => x.IsАrchived == false && x.IsDeleted == false)
                   .FirstOrDefault();

            if (advert == null)
            {
                return false;
            }

            advert.IsАrchived = true;
            advert.ModifiedOn = DateTime.UtcNow;

            context.SaveChanges();

            observeService.SendEmailOUpdate(advertId, emailTitle, emailAdvertArchiveContent);

            return true;
        }
        public IEnumerable<AdvertShowingViewModel> GetArchiveAdverts()
        {
            var userArchiev = context.Adverts
                 .Where(x => x.UserId == userService.GetCurrentUserId() && x.IsАrchived == true && x.IsDeleted == false)
                 .Select(s => new AdvertShowingViewModel
                 {
                     Id = s.Id,
                     Description = s.Description,
                     ImageURL = s.AdvertImages.First().ImageUrl,
                     Name = s.Name,
                     Price = s.Price,
                 })
                 .ToList();

            return userArchiev;
        }
        public bool IsArchiveFull()
        {
            var archivedAdverts = context.Adverts
                .Where(x => x.UserId == userService.GetCurrentUserId() && x.IsАrchived == true && x.IsDeleted == false)
                .Select(x => x.Name)
                .ToList();

            if (archivedAdverts.Count == maxArchiveAdvertsPerUser)
            {
                return true;
            }

            return false;
        }

        public bool UploadAdvert(string advertId)
        {
            var advert = context.Adverts
                   .Where(x => x.Id == advertId && x.UserId == userService.GetCurrentUserId())
                   .Where(x => x.IsАrchived == true && x.IsDeleted == false)
                   .FirstOrDefault();

            if (advert == null)
            {
                return false;
            }

            advert.IsАrchived = false;
            advert.ModifiedOn = DateTime.UtcNow;

            context.SaveChanges();

            observeService.SendEmailOUpdate(advertId, emailTitle, emailAdvertReUploadContent);

            return true;
        }

        public ArchiveAdvertDetailModel GetArchivedAdvert(string advertId)
        {
            var advert = context.Adverts
                   .Where(x => x.Id == advertId && x.UserId == userService.GetCurrentUserId())
                   .Where(x => x.IsАrchived == true && x.IsDeleted == false)
                   .Include(x => x.AdvertImages)
                   .FirstOrDefault();

            if (advert == null)
            {
                return null;
            }

            var archiveAdvert = new ArchiveAdvertDetailModel
            {
                Id = advert.Id,
                Name = advert.Name,
                Description = advert.Description,
                CreatedOn = advert.CreatedOn,
                Price = advert.Price,
                Images = advert.AdvertImages.Select(s => s.ImageUrl).ToList(),
            };

            var advertStatistics = new ArchiveAdvertStatisticsMode
            {
                TotalView = advert.Viewed,
                FollowedBy = GetFollowers(advertId),
                IntrestedPeople = GetIntrestedPeople(advertId),
            };

            archiveAdvert.Statistics = advertStatistics;

            return archiveAdvert;
        }

        private int GetFollowers(string advertId)
        => context.Adverts.Find(advertId).ОbservedAdverts.Count;

        private int GetIntrestedPeople(string advertId)
       => context.Adverts.Find(advertId).Messages.GroupBy(x => x.SenderId).ToList().Count;

    }
}
