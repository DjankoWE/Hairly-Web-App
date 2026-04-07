using System.ComponentModel.DataAnnotations;
using static Hairly.GCommon.ValidationConstants;

namespace Hairly.Data.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClientId { get; set; }
        public virtual Client Client { get; set; } = null!;

        [Required]
        public int AppointmentId { get; set; }
        public virtual Appointment Appointment { get; set; } = null!;

        [Required]
        [Range(ReviewRatingMin, ReviewRatingMax)]
        public int Rating { get; set; }

        [MaxLength(ReviewCommentMaxLength)]
        public string? Comment { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}
