using Realdeal.Models.Advert;
using System.Collections.Generic;

namespace Realdeal.Service.Archive
{
    public interface IArchiveService
    {
        public bool IsArchiveFull();
        public bool AddAdvertToArchive(string advertId);
        public bool UploadAdvert(string advertId);
        public IEnumerable<AdvertShowingViewModel> GetArchiveAdverts();
    }
}
