using Realdeal.Models.Advert;
using System.Collections.Generic;
using static Realdeal.Common.GlobalConstants;

namespace Realdeal.Service.Advert
{
    public interface IAdvertService
    {
        void CreateAdvert(AdvertFormModel advert);
        bool DeleteAdvert(string advertId);
        string GetAdvertName(string advertId);

        AllAdvertsQueryModel GetAllAdverts(AllAdvertsQueryModel queryAdverts);
        public AdvertDetailViewModel GetAdvertDetailsById(string advertId);
        public AdvertEditFormModel FindAdvertToEdit(string advertId);
        public bool EditAdvert(AdvertEditFormModel advertEdit);
        public UserAdvertsModel GetUserAdvertById(string userId);
        public IEnumerable<AdvertShowingViewModel> GetNewestAdverts(int count = maxNewAdvertOnHomePage);
    }
}
