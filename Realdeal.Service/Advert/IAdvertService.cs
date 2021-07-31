using Realdeal.Models.Advert;

namespace Realdeal.Service.Advert
{
    public interface IAdvertService
    {
        void CreateAdvert(AdvertFormModel advert);
        AllAdvertsQueryModel GetAllAdverts(AllAdvertsQueryModel queryAdverts);
        public AdvertDetailViewModel GetAdvertById(string advertId);
    }
}
