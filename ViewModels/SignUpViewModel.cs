using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MoonlightShadow.ViewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "Wprowadź login")]
        [Display(Name = "Login: ")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Wprowadź adres e-mail")]
        [EmailAddress(ErrorMessage = "Wprowadź poprawny e-mail")]
        [Display(Name = "Email: ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Wprowadź hasło")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Hasło musi zawierać od 8 do 15 znaków")]
        [RegularExpression(@"^\w{8,15}$", ErrorMessage = "Hasło może zawierać jedynie znaki alfanumeryczne")]
        [Display(Name = "Hasło: ")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Wprowadź powtórnie hasło")]
        [Display(Name = "Hasło 2: ")]
        public string RepetedPassword { get; set; }
    }
}