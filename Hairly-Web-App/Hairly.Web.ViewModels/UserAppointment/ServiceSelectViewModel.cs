namespace Hairly.Web.ViewModels.UserAppointment
{
    public class ServiceSelectViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int DurationInMinutes { get; set; }
    }
}
