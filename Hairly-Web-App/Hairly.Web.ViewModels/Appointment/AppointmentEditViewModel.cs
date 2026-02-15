using System.ComponentModel.DataAnnotations;
using Hairly.Data.Models.Enums;
using static Hairly.GCommon.ValidationConstants;

namespace Hairly.Web.ViewModels.Appointment
{
    public class AppointmentEditViewModel
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public int ServiceId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public AppointmentStatus Status { get; set; }

        [MaxLength(AppointmentNotesMaxLength)]
        public string? Note { get; set; }

        public Dictionary<int, string> Clients { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, string> Services { get; set; } = new Dictionary<int, string>();
    }
}