using System.ComponentModel.DataAnnotations;
using Hairly.Data.Models.Enums;

namespace Hairly.Web.ViewModels.Appointment
{
    public class AppointmentDeleteViewModel
    {
        public int Id { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public string ClientFullName { get; set; } = null!;

        [Required]
        public string ServiceName { get; set; } = null!;

        [Required]
        public AppointmentStatus Status { get; set; }
    }
}
