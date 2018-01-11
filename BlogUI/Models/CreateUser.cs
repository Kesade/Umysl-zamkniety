using System.ComponentModel.DataAnnotations;
using System.Web.Security;
using Common.UI;

namespace BlogUI.Models
{
    public class CreateUser : ICreateUser
    {
        [Compare("Password")]
        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 8)]
        [Required]
        [MembershipPassword]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 8)]
        [Required]
        [MembershipPassword]
        public string Password { get; set; }

        public string Roles { get; set; }

        [DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 6)]
        [Required]
        public string Login { get; set; }

        [DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 6)]
        [Required]
        public string Name { get; set; }
    }
}