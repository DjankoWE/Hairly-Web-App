using System.ComponentModel.DataAnnotations;
using static Hairly.GCommon.ValidationConstants;

namespace Hairly.Web.ViewModels.Service
{
    public class ServiceEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ServiceNameMinLength)]
        [MaxLength(ServiceNameMaxLength)]
        public string Name { get; set; } = null!;
        
        [MinLength(ServiceDescriptionMinLength)]
        [MaxLength(ServiceDescriptionMaxLength)]
        public string? Description { get; set; }

        [Range(typeof(decimal), ServicePriceMinValue, ServicePriceMaxValue)]
        public decimal Price { get; set; }

        [Range(ServiceDurationInMinutesMinValue, ServiceDurationInMinutesMaxValue)]
        public int DurationInMinutes { get; set; }
    }
}
