namespace Hairly.Web.ViewModels.Review
{
    public class ReviewIndexViewModel
    {
        public int Id { get; set; }

        public int Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ClientName { get; set; } = null!;

        public string HairdresserName { get; set; } = null!;

        public string ServiceName { get; set; } = null!;
    }
}
