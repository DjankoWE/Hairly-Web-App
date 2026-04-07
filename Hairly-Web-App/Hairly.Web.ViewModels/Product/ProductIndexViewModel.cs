namespace Hairly.Web.ViewModels.Product
{
    public class ProductIndexViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int QuantityInStock { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsDeleted { get; set; }
    }
}
