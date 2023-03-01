using System;
using System.ComponentModel.DataAnnotations;

namespace MoonlightShadow.ViewModels
{
    public class RemindMePasswordViewModel
    {
        [Required(ErrorMessage = "Wprowadź adres e-mail")]
        [EmailAddress(ErrorMessage = "Email niepoprawny")]
        public string Email { get; set; }
    }
}