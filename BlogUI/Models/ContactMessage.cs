using System.ComponentModel.DataAnnotations;

namespace BlogUI.Models
{
    public class ContactMessage : Common.UI.IEmailMessage
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }
    }
}