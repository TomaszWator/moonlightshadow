using System;
using System.ComponentModel.DataAnnotations;

namespace MoonlightShadow.ViewModels
{
    public class LoginFormViewModel
    {
        [Required(ErrorMessage = "Wprowadź adres e-mail")]
        [EmailAddress(ErrorMessage = "Email nie jest poprawny (może brakuje małpy lub kropki)")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Wprowadź hasło")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Hasło musi zawierać od 8 do 15 znaków")]
        [RegularExpression(@"^\w{8,15}$", ErrorMessage = "Hasło może zawierać jedynie znaki alfanumeryczne")]
        [Display(Name = "Hasło: ")]
        public string Password { get; set; }
    }
}