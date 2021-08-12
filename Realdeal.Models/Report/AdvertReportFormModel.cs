using System.ComponentModel.DataAnnotations;
using static Realdeal.Data.DataConstants;

namespace Realdeal.Models.Report
{
    public class AdvertReportFormModel
    {
        public string AdvertId { get; set; }

        [Display(Name ="Advert name")]
        public string AdvertName { get; set; }

        [Required]
        [StringLength(reportDescriptionMaxLenght,
            MinimumLength = reportDescriptionMinLenght,
             ErrorMessage = "The  description minimum lenght must be {2}")]
        public string Description { get; set; }
    }
}
