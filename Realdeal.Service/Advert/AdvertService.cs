using Realdeal.Data;
using Realdeal.Data.Models;
using Realdeal.Models.Advert;
using Realdeal.Models.Advert.Enum;
using Realdeal.Service.CloudinaryCloud;
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

        public AllAdvertsQueryModel GetAllAdverts(AllAdvertsQueryModel queryAdverts)
        {
            var adverQuery = context.Adverts.Where(x=>x.IsDeleted==false && x.IsАrchived==false).AsQueryable();

            if (!string.IsNullOrEmpty(queryAdverts.Search))
            {
                adverQuery = adverQuery.Where(x => x.Name.ToLower().Contains(queryAdverts.Search));
            }

            if (!string.IsNullOrEmpty(queryAdverts.CategoryId))
            {
                adverQuery = adverQuery.Where(x => x.SubCategoryId == queryAdverts.CategoryId);
            }

            adverQuery = queryAdverts.AdvertSorting switch
            {
                AdvertSorting.DateCreated => adverQuery.OrderBy(x => x.CreatedOn),
                AdvertSorting.NameDescending => adverQuery.OrderByDescending(x => x.Name),
                AdvertSorting.NameАscending => adverQuery.OrderBy(x => x.Name),
                AdvertSorting.priceDescending => adverQuery.OrderByDescending(x => x.Price),
                AdvertSorting.PrieceAscending => adverQuery.OrderBy(x => x.Price),
                _ => adverQuery.OrderByDescending(x => x.CreatedOn),
            };


            queryAdverts.Advers = adverQuery.Select(x => new AdvertShowingViewModel
            {
                Name = x.Name,
                Id = x.Id,
                Description = x.Description,
                ImageURL = x.AdvertImages.FirstOrDefault().ImageUrl,
                Price = x.Price
            }).ToList();

            return queryAdverts;
        }

        public AdvertDetailViewModel GetAdvertById(string advertId)
        {
            return context.Adverts.Where(x=>x.Id==advertId).Select(s=>new AdvertDetailViewModel
            {
                Name=s.Name,
                CreatedOn=s.CreatedOn,
                Description=s.Description,
                Id=advertId,
                Images=s.AdvertImages.Select(i=>i.ImageUrl).ToList(),
                Price=s.Price,
            }).FirstOrDefault();
        }
    }
}
