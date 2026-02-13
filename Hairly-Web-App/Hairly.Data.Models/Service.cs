using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using static Hairly.GCommon.ValidationConstants;

namespace Hairly.Data.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ServiceNameMaxLength)]
        public string Name { get; set; } = null!;

        [MaxLength(ServiceDescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public int DurationInMinutes { get; set; }

        public bool IsDeleted { get; set; } = false;

        [Required] 
        public string HairdresserId { get; set; } = null!;
        public virtual IdentityUser Hairdresser { get; set; } = null!;

        public virtual ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
    }
}
