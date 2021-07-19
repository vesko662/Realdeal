using System.ComponentModel.DataAnnotations;
using static Realdeal.Data.DataConstants;

namespace Realdeal.Web.Models.Advert
{
    public class AdvertFormModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name="Category")]
        public string CategoryId { get; set; }

        public decimal Price { get; set; }

    }
}
