using System.ComponentModel.DataAnnotations;
using static Hairly.GCommon.ValidationConstants;

namespace Hairly.Web.ViewModels.Review
{
    public class ReviewCreateViewModel
    {
        public int AppointmentId { get; set; }

        public string ServiceName { get; set; } = null!;

        public string HairdresserName { get; set; } = null!;

        public DateTime AppointmentDate { get; set; }

        [Required]
        [Range(ReviewRatingMin, ReviewRatingMax)]
        public int Rating { get; set; }

        [MaxLength(ReviewCommentMaxLength)]
        public string? Comment { get; set; }
    }
}
