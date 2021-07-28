using Realdeal.Models.Advert;
using System.Collections.Generic;

namespace Realdeal.Service.Advert
{
    public interface IAdvertService
    {
        void CreateAdvert(AdvertFormModel advert, string userId);
        IEnumerable<AdvertShowingViewModel> GetAllAdvers();
    }
}
