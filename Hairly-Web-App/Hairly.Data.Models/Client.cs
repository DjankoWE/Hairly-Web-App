using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using static Hairly.GCommon.ValidationConstants;

namespace Hairly.Data.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ClientFirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(ClientLastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [RegularExpression(ClientPhoneNumberRegex)]
        public string PhoneNumber { get; set; } = null!;

        [MaxLength(ClientEmailMaxLength)]
        public string? Email { get; set; }

        [MaxLength(ClientNotesMaxLength)]
        public string? Note { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; } = false;

        [Required]
        public string HairdresserId { get; set; } = null!;
        public virtual IdentityUser Hairdresser { get; set; } = null!;

        public virtual ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
    }
}
