using Hairly.Data.Models.Enums;

namespace Hairly.Web.ViewModels.Appointment
{
    public class AppointmentIndexViewModel
    {
        public int Id { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string ClientFullName { get; set; } = null!;

        public string ServiceName { get; set; } = null!;

        public decimal ServicePrice { get; set; }

        public int ServiceDuration { get; set; }

        public AppointmentStatus Status { get; set; }
    }
}
