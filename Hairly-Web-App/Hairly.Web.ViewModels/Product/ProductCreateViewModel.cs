using System.ComponentModel.DataAnnotations;
using static Hairly.GCommon.ValidationConstants;

namespace Hairly.Web.ViewModels.Product
{
    public class ProductCreateViewModel
    {
        [Required]
        [MinLength(ProductNameMinLength)]
        [MaxLength(ProductNameMaxLength)]
        public string Name { get; set; } = null!;

        [MaxLength(ProductDescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        [Range(typeof(decimal), ProductPriceMinValue, ProductPriceMaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(ProductQuantityMin, ProductQuantityMax)]
        public int QuantityInStock { get; set; }

        [MaxLength(ProductImageUrlMaxLength)]
        public string? ImageUrl { get; set; }
    }
}
