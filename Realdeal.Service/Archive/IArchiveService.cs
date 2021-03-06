using Realdeal.Models.Advert;
using Realdeal.Models.Archive;
using System.Collections.Generic;

namespace Realdeal.Service.Archive
{
    public interface IArchiveService
    {
        public bool IsArchiveFull();
        public bool AddAdvertToArchive(string advertId);
        public bool UploadAdvert(string advertId);
        public IEnumerable<AdvertShowingViewModel> GetArchiveAdverts();
        public ArchiveAdvertDetailModel GetArchivedAdvert(string advertId);
    }
}
