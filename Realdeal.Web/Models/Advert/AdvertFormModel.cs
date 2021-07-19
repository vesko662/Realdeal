﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Realdeal.Data.DataConstants;

namespace Realdeal.Web.Models.Advert
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
        [Url]
        [Display(Name = "Image URL")]
        public string ImgUrl { get; set; }

        [Required]
        [Range(typeof(decimal), advertPriceMinValue, advertPriceMaxValue, ErrorMessage = "The advert price is invalid")]
        public decimal Price { get; set; }

        public Dictionary<string, IEnumerable<AdvertCategoryViewModel>> Categories { get; set; }
    }
}
