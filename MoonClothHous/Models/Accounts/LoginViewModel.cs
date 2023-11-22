using System.ComponentModel.DataAnnotations;

namespace MoonClothHous.Models.Accounts
{
    public class LoginViewModel
    {
        [Required] // You can use EmailAddress attribute for email validation
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

}
