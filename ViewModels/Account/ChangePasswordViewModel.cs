using System;
using System.ComponentModel.DataAnnotations;

namespace MoonlightShadow.ViewModels.Account
{
    public class ChangePasswordViewModel
    {
        [Display(Name = "Email: ")]
        [Required(ErrorMessage = "Wprowadź email")]
        public string Email { get; set; }

        [Display(Name = "Stare hasło: ")]
        [Required(ErrorMessage = "Wprowadź hasło")]
        public string Password { get; set; }

        [Display(Name = "Nowe hasło: ")]
        [Required(ErrorMessage = "Wprowadź hasło")]
        public string NewPassword { get; set; }

        [Display(Name = "Powtórz hasło: ")]
        [Required(ErrorMessage = "Wprowadź powtórzone hasło")]
        public string RepetedPassword { get; set; }
    }
}