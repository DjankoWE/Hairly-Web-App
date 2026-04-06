using System.ComponentModel.DataAnnotations;
using static Hairly.GCommon.ValidationConstants;

namespace Hairly.Web.ViewModels.UserAppointment
{
    public class UserAppointmentBookViewModel
    {
        [Required]
        public int ServiceId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [MaxLength(UserAppointmentNotesMaxLength)]
        public string? Note { get; set; }

        public IEnumerable<ServiceSelectViewModel> AvailableServices { get; set; } = new List<ServiceSelectViewModel>();
    }
}
