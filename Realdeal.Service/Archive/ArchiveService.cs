using Realdeal.Data;
using System;

namespace Realdeal.Service.Archive
{
    public class ArchiveService : IArchiveService
    {
        private readonly RealdealDbContext context;

        public ArchiveService(RealdealDbContext context)
        {
            this.context = context;
        }

        public bool AddAdvertToArchive(string advertId)
        {
            var advert = context.Adverts.Find(advertId);

            if (advert==null)
            {
                return false;
            }

            advert.IsАrchived = true;
            context.SaveChanges();

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
