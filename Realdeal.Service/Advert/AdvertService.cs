using Realdeal.Data;
using Realdeal.Data.Models;
using Realdeal.Models.Advert;
using Realdeal.Service.CloudinaryCloud;

namespace Realdeal.Service.Advert
{
    public class AdvertService : IAdvertService
    {
        private readonly RealdealDbContext context;
        private readonly ICloudinaryService cloudinary;

        public AdvertService(RealdealDbContext context, ICloudinaryService cloudinary)
        {
            this.context = context;
            this.cloudinary = cloudinary;
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

            foreach (var item in advertModel.Images)
            {
                var a = cloudinary.UploadPhoto(item, "advertImages");
            }

            #warning Add photo to db

            context.Adverts.Add(advert);
            context.SaveChanges();
        }
    }
}
