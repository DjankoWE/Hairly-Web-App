namespace Hairly.Web.ViewModels.Client
{
    public class ClientDeleteViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public int TotalAppointments { get; set; }
    }
}
