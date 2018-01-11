using System.ComponentModel.DataAnnotations;

namespace BlogUI.Models
{
    public class LoginUser
    {
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [DataType(DataType.Text)]
        [Required]
        public string Login { get; set; }
    }
}