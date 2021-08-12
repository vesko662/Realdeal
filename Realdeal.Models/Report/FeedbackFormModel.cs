using System.ComponentModel.DataAnnotations;
using static Realdeal.Data.DataConstants;

namespace Realdeal.Models.Report
{
    public class FeedbackFormModel
    {
        [Required]
        [Display(Name ="Feedback")]
        [StringLength(reportDescriptionMaxLenght,
           MinimumLength = reportDescriptionMinLenght,
            ErrorMessage = "The  description minimum lenght must be {2}")]
        public string Description { get; set; }
    }
}
