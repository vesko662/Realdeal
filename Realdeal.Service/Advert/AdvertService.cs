using Realdeal.Data;
using Realdeal.Data.Models;
using Realdeal.Models.Advert;

namespace Realdeal.Service.Advert
{
    public class AdvertService : IAdvertService
    {
        private readonly RealdealDbContext context;

        public AdvertService(RealdealDbContext context)
        {
            this.context = context;
        }

        public void CreateAdvert(AdvertFormModel advertModel, string userId)
        {
            var advert = new Data.Models.Advert
            {
                Name = advertModel.Name,
                Description = advertModel.Description,
                SubCategoryId = advertModel.CategoryId,
                Price = advertModel.Price,
                UserId = userId
            };

            context.Adverts.Add(advert);
            context.SaveChanges();
        }
    }
}
