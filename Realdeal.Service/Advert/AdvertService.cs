using Realdeal.Data;
using Realdeal.Data.Models;
using Realdeal.Models.Advert;
using Realdeal.Service.CloudinaryCloud;
using System.Collections.Generic;
using System.Linq;

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

            foreach (var image in advertModel.Images)
            {
                advert.AdvertImages.Add(new AdvertImage() { ImageUrl = cloudinary.UploadPhoto(image, "advertImages") });
            }

            context.Adverts.Add(advert);
            context.SaveChanges();
        }

        public IEnumerable<AdvertShowingViewModel> GetAllAdvers()
        {
            return context.Adverts
                .Select(x => new AdvertShowingViewModel
                {
                    Name = x.Name,
                    Id = x.Id,
                    Description = x.Description.Substring(0,20) +" "+"....",
                    ImageURL = x.AdvertImages.FirstOrDefault().ImageUrl,
                    Price = x.Price
                })
                .ToList();
        }
    }
}
