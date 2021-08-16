﻿using System.ComponentModel.DataAnnotations;
using static Realdeal.Data.DataConstants;

namespace Realdeal.Models.Category
{
    public class CategoryEditFormModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [StringLength(categoryNameMaxLenght, MinimumLength = defaultMinLenght, ErrorMessage = "Category name must be string of lenght between {2} and {1}")]
        public string Name { get; set; }
    }
}
