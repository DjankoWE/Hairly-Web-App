using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Hairly.Data.Models.Enums;
using static Hairly.GCommon.ValidationConstants;

namespace Hairly.Data.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public int ClientId { get; set; } 
        public virtual Client Client { get; set; } = null!;

        [Required]
        public int ServiceId { get; set; }
        public virtual Service Service { get; set; } = null!;

        [Required]
        public string HairdresserId { get; set; } = null!;
        public virtual IdentityUser Hairdresser { get; set; } = null!;

        [MaxLength(AppointmentNotesMaxLength)]
        public string? Note { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required] 
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;

        public bool IsDeleted { get; set; } = false;
    }
}
