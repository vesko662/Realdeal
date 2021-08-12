using Realdeal.Models.User;
using System.Collections.Generic;

namespace Realdeal.Models.Advert
{
    public class UserAdvertsModel
    {
        public UserInformationModel User { get; set; }
        public IEnumerable<AdvertShowingViewModel> Adverts { get; set; }

    }
}
