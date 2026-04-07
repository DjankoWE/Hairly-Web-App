namespace Hairly.Web.ViewModels.Product
{
    public class ProductDeleteViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int QuantityInStock { get; set; }
    }
}
