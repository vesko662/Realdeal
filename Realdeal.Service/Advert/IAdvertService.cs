using Realdeal.Models.Advert;

namespace Realdeal.Service.Advert
{
    public interface IAdvertService
    {
        void CreateAdvert(AdvertFormModel advert);
        bool DeleteAdvert(string advertId);

        AllAdvertsQueryModel GetAllAdverts(AllAdvertsQueryModel queryAdverts);
        public AdvertDetailViewModel GetAdvertDetailsById(string advertId);
        public AdvertEditFormModel FindAdvertToEdit(string advertId);
        public bool EditAdvert(AdvertEditFormModel advertEdit);
    }
}
