using System.ComponentModel.DataAnnotations;
using static Hairly.GCommon.ValidationConstants;

namespace Hairly.Web.ViewModels.Client
{
    public class ClientCreateViewModel
    {
        [Required]
        [MinLength(ClientFirstNameMinLength)]
        [MaxLength(ClientFirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MinLength(ClientLastNameMinLength)]
        [MaxLength(ClientLastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [RegularExpression(ClientPhoneNumberRegex, ErrorMessage = "Invalid phone number. Use format: +359... or 08...")]
        public string PhoneNumber { get; set; } = null!;

        [EmailAddress]
        [MaxLength(ClientEmailMaxLength)]
        public string? Email { get; set; }

        [MaxLength(ClientNotesMaxLength)]
        public string? Note { get; set; }
    }
}
