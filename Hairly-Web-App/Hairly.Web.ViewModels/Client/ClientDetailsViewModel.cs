namespace Hairly.Web.ViewModels.Client
{
    public class ClientDetailsViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string FullName => $"{FirstName} {LastName}";

        public string PhoneNumber { get; set; } = null!;

        public string? Email { get; set; }

        public string? Note { get; set; }

        public DateTime CreatedOn { get; set; }

        public List<ClientAppointmentViewModel> RecentAppointments { get; set; } = new List<ClientAppointmentViewModel>();
    }
}
