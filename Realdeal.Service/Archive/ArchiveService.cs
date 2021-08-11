using Realdeal.Data;
using static Realdeal.Common.GlobalConstants;
using Realdeal.Service.User;
using System.Linq;
using System.Collections.Generic;
using Realdeal.Models.Advert;
using System;

namespace Realdeal.Service.Archive
{
    public class ArchiveService : IArchiveService
    {
        private readonly RealdealDbContext context;
        private readonly IUserService userService;

        public ArchiveService(RealdealDbContext context, IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        public bool AddAdvertToArchive(string advertId)
        {
            var advert = context.Adverts
                .Where(x => x.Id == advertId && x.UserId==userService.GetCurrentUserId())
                .FirstOrDefault();

            if (advert == null)
            {
                return false;
            }

            advert.IsАrchived = true;
            advert.ModifiedOn = DateTime.UtcNow;
            context.SaveChanges();

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
                .FirstOrDefault(); ;

            if (advert == null)
            {
                return false;
            }

            advert.IsАrchived = false;
            advert.ModifiedOn = DateTime.UtcNow;
            context.SaveChanges();

            return true;
        }

    }
}
