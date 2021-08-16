using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Realdeal.Data.DataConstants;

namespace Realdeal.Models.Category
{
    public class CreateSubCategoryFormModel
    {
        [Required]
        [StringLength(categoryNameMaxLenght, MinimumLength = defaultMinLenght, ErrorMessage = "Category name must be string of lenght between {2} adn {1}")]
        public string Name { get; set; }
        [Required]
        [Display(Name="Assign to")]
        public string MainCategoryId { get; set; }
        public IEnumerable<CategoryModel> MainCategories { get; set; }
    }
}
