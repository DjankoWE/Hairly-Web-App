using System.ComponentModel.DataAnnotations;
using static Hairly.GCommon.ValidationConstants;

namespace Hairly.Web.ViewModels.Appointment
{
    public class AppointmentCreateViewModel
    {
        public int ClientId { get; set; }

        public int ServiceId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [MaxLength(AppointmentNotesMaxLength)]
        public string? Note { get; set; }

        public virtual Dictionary<int, string> Clients { get; set; } = new Dictionary<int, string>();
        public virtual Dictionary<int, string> Services { get; set; } = new Dictionary<int, string>();
    }
}
