namespace Hairly.Web.ViewModels.Service
{
    public class ServiceDeleteViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int ActiveAppointments { get; set; }
    }
}
