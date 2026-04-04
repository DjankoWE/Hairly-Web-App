namespace Hairly.Web.ViewModels.Product
{
    public class ProductDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int QuantityInStock { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
