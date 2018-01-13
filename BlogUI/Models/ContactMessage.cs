using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Security.Policy;

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

        public string Topic { get; set; }
        public string MailTo { get; set; }
    }
}