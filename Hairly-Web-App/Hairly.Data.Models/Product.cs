using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Hairly.GCommon.ValidationConstants;

namespace Hairly.Data.Models
{
    public class Product
    {
        [Key] 
        public int Id { get; set; }

        [Required]
        [MaxLength(ProductNameMaxLength)]
        public string Name { get; set; } = null!;

        [MaxLength(ProductDescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal Price { get; set; }

        [Required] 
        public int QuantityInStock { get; set; }

        [MaxLength(ProductImageUrlMaxLength)] 
        public string? ImageUrl { get; set; }

        [Required] 
        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}