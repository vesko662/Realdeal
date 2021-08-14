using Microsoft.EntityFrameworkCore;
using static Realdeal.Common.GlobalConstants;
using Realdeal.Data;
using Realdeal.Data.Models;
using Realdeal.Models.Advert;
using Realdeal.Models.Advert.Enum;
using Realdeal.Service.CloudinaryCloud;
using Realdeal.Service.User;
using System.Linq;
using System;
using Realdeal.Service.Observe;

namespace Realdeal.Service.Advert
{
    public class AdvertService : IAdvertService
    {
        private readonly RealdealDbContext context;
        private readonly ICloudinaryService cloudinary;
        private readonly IUserService userService;
        private readonly IObserveService observeService;

        public AdvertService(RealdealDbContext context,
            ICloudinaryService cloudinary,
            IUserService userService,
            IObserveService observeService)
        {
            this.context = context;
            this.cloudinary = cloudinary;
            this.userService = userService;
            this.observeService = observeService;
        }
        public void CreateAdvert(AdvertFormModel advertModel)
        {
            var advert = new Data.Models.Advert
            {
                Name = advertModel.Name,
                Description = advertModel.Description,
                SubCategoryId = advertModel.CategoryId,
                Price = advertModel.Price,
                UserId = userService.GetCurrentUserId(),
            };

            foreach (var image in advertModel.Images)
            {
                advert.AdvertImages.Add(new AdvertImage() { ImageUrl = cloudinary.UploadPhoto(image, cloudFolderForAdvertImages) });
            }

            context.Adverts.Add(advert);
            context.SaveChanges();
        }

        public AllAdvertsQueryModel GetAllAdverts(AllAdvertsQueryModel queryAdverts)
        {
            var adverQuery = context.Adverts.Where(x => x.IsDeleted == false && x.IsАrchived == false).AsQueryable();

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

        public AdvertDetailViewModel GetAdvertDetailsById(string advertId)
        {
            var advert = context.Adverts
                .Where(x => x.Id == advertId && x.IsDeleted == false && x.IsАrchived == false)
                .Select(s => new AdvertDetailViewModel
                {
                    Name = s.Name,
                    CreatedOn = s.CreatedOn,
                    Description = s.Description,
                    Id = advertId,
                    Images = s.AdvertImages.Select(i => i.ImageUrl).ToList(),
                    Price = s.Price,
                    IsObserved = s.ОbservedAdverts.Contains(new ОbservedAdvert { UserId = userService.GetCurrentUserId(), AdvertId = advertId }),
                    User = userService.GetUserInfo(userService.GetUserIdByAdvertId(advertId)),
                })
                .FirstOrDefault();

            if (advert == null)
            {
                return null;
            }

            UpdateViewsOnAdvert(advertId);

            return advert;
        }

        public bool DeleteAdvert(string advertId)
        {
            var adver = context.Adverts.Find(advertId);

            if (adver == null)
            {
                return false;
            }

            adver.IsDeleted = true;
            adver.ModifiedOn = DateTime.UtcNow;

            context.SaveChanges();

            observeService.SendEmailOUpdate(advertId, emailTitle, emailAdvertDeleteContent);
            observeService.RemoveAllObservingUsers(advertId);

            return true;
        }

        public AdvertEditFormModel FindAdvertToEdit(string advertId)
        => context.Adverts.Where(x => x.Id == advertId).Select(s => new AdvertEditFormModel
        {
            Id = s.Id,
            CategoryId = s.SubCategoryId,
            Description = s.Description,
            Name = s.Name,
            Price = s.Price,
        })
            .FirstOrDefault();

        public bool EditAdvert(AdvertEditFormModel advertEdit)
        {
            var advert = context.Adverts.Include(x => x.AdvertImages).FirstOrDefault(x => x.Id == advertEdit.Id);

            if (advert == null)
            {
                return false;
            }

            advert.Name = advertEdit.Name;
            advert.Description = advertEdit.Description;
            advert.SubCategoryId = advertEdit.CategoryId;
            advert.Price = advertEdit.Price;
            advert.ModifiedOn = DateTime.UtcNow;

            if (advertEdit.Images != null)
            {
                advert.AdvertImages.Clear();

                foreach (var image in advertEdit.Images)
                {
                    advert.AdvertImages.Add(new AdvertImage() { ImageUrl = cloudinary.UploadPhoto(image, cloudFolderForAdvertImages) });
                }
            }

            context.SaveChanges();

            observeService.SendEmailOUpdate(advertEdit.Id, emailTitle, emailAdvertUpdateContent);

            return true;
        }

        private void UpdateViewsOnAdvert(string advertId)
        {
            var advert = context.Adverts.Find(advertId);

            advert.Viewed += 1;

            context.SaveChanges();
        }

        public UserAdvertsModel GetUserAdvertById(string userId)
        {
            var adverts = context.Adverts
                .Where(x => x.UserId == userId)
                .Where(x => x.IsDeleted == false && x.IsАrchived == false)
                .Select(s => new AdvertShowingViewModel()
                {
                    Id = s.Id,
                    Description = s.Description,
                    ImageURL = s.AdvertImages.FirstOrDefault().ImageUrl,
                    Name = s.Name,
                    Price = s.Price,
                })
                .ToList();

            if (adverts == null)
            {
                return null;
            }

            return new UserAdvertsModel()
            {
                Adverts = adverts,
                User = userService.GetUserInfo(userId),
            };
        }
    }
}
