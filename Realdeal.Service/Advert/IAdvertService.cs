using Realdeal.Models.Advert;

namespace Realdeal.Service.Advert
{
    public interface IAdvertService
    {
        void CreateAdvert(AdvertFormModel advert, string userId);
        AllAdvertsQueryModel GetAllAdverts(AllAdvertsQueryModel queryAdverts);
    }
}
