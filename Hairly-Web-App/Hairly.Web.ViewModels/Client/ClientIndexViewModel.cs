namespace Hairly.Web.ViewModels.Client
{
    public class ClientIndexViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string FullName => $"{FirstName} {LastName}";

        public string PhoneNumber { get; set; } = null!;

        public string? Email { get; set; }

        public int TotalAppointments { get; set; }
    }
}
