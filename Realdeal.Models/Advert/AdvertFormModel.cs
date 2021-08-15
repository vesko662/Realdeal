using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Realdeal.Data.DataConstants;
using Microsoft.AspNetCore.Http;
using Realdeal.Models.Category;

namespace Realdeal.Models.Advert
{
    public class AdvertFormModel
    {
        [Required]
        [StringLength(advertNameMaxLenght, MinimumLength = defaultMinLenght)]
        public string Name { get; set; }

        [Required]
        [StringLength(advertDescriptionMaxLenght, MinimumLength = advertDescriptionMinLenght, ErrorMessage = "The advert description minimum  lenght must be {2} ")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string CategoryId { get; set; }

        [Required]
        [Display(Name = "Images")]
        public List<IFormFile> Images{ get; set; }

        [Required]
        [Range(typeof(decimal), advertPriceMinValue, advertPriceMaxValue, ErrorMessage = "The advert price is invalid")]
        public decimal Price { get; set; }

        public IEnumerable<MainCategoriesShowingViewModel> Categories { get; set; }
    }
}
