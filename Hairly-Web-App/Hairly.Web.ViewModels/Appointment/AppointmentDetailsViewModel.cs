using System.ComponentModel.DataAnnotations;
using Hairly.Data.Models.Enums;

namespace Hairly.Web.ViewModels.Appointment
{
    public class AppointmentDetailsViewModel
    {
        public int Id { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public AppointmentClientInfo Client { get; set; } = null!;

        [Required]
        public AppointmentServiceInfo Service { get; set; } = null!;

        [Required]
        public AppointmentStatus Status { get; set; }

        public string? Note { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

    }

    public class AppointmentClientInfo
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        public string PhoneNumber { get; set; } = null!;

        public string? Email { get; set; }
    }

    public class AppointmentServiceInfo
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        public int DurationInMinutes { get; set; }
    }
}
