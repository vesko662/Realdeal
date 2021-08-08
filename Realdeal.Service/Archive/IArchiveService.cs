namespace Realdeal.Service.Archive
{
    public interface IArchiveService
    {
        public bool IsArchiveFull();
        public bool AddAdvertToArchive(string advertId);
        public bool UploadAdvert(string advertId);

    }
}
