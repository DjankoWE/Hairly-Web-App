using Hairly.Data.Models.Enums;

namespace Hairly.Web.ViewModels.UserAppointment
{
    public class UserAppointmentIndexViewModel
    {
        public int Id { get; set; }

        public string ServiceName { get; set; } = null!;

        public string HairdresserName { get; set; } = null!;

        public DateTime AppointmentDate { get; set; }

        public AppointmentStatus Status { get; set; }

        public bool HasReview { get; set; }
    }
}
