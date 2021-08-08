using Realdeal.Data;
using static Realdeal.Common.GlobalConstants;
using System;
using Realdeal.Service.User;
using System.Linq;

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
            var advert = context.Adverts.Find(advertId);

            if (advert == null)
            {
                return false;
            }

            advert.IsАrchived = true;
            context.SaveChanges();

            return true;
        }

        public bool IsArchiveFull()
        {
            var archivedAdverts = context.Adverts
                .Where(x => x.UserId == userService.GetCurrentUserId() && x.IsАrchived == true)
                .ToList();

            if (archivedAdverts.Count > maxArchiveAdvertsPerUser)
            {
                return false;
            }

            return true;
        }

        public bool UploadAdvert(string advertId)
        {
            var advert = context.Adverts.Find(advertId);

            if (advert == null)
            {
                return false;
            }

            advert.IsАrchived = false;
            context.SaveChanges();

            return true;
        }
    }
}
