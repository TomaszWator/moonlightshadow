using System;
using System.ComponentModel.DataAnnotations;

namespace MoonlightShadow.ViewModels.Account
{
    public class ShippingDataViewModel
    {
        [Display(Name = "Imie")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Display(Name = "Email do wysyłki")]
        [EmailAddress(ErrorMessage = "Wprowadzono niepoprawny email")]
        public string EmailShipping { get; set; }

        [Display(Name = "Numer telefonu")]
        [RegularExpression(@"(^$|^(([\+][(][0-9]{2}[)]|[\+]?[0-9]{2})?[\s]?(([0-9]{3}[-\s]?[0-9]{3}[-\s]?[0-9]{3})|(([(][0-9]{2}[)]|[0-9]{2})?[\s]?[\d]{2}[-\s]?([\d]{2}[-\s]?[\d]{3}|[\d]{3}[-\s]?[\d]{2}))))$)", 
            ErrorMessage = "Numer telefonu nie jest poprawny")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Kraj")]
        public string Country { get; set; }

        [Display(Name = "Ulica")]
        public string Street { get; set; }

        [Display(Name = "Miejscowość")]
        public string Town { get; set; }

        [Display(Name = "Województwo")]
        public string State { get; set; }

        [Display(Name = "Kod pocztowy")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Wprowadź hasło")]
        [RegularExpression(@"^\w{8,15}$", ErrorMessage = "Hasło może zawierać jedynie znaki alfanumeryczne")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Hasło musi zawierać od 8 do 15 znaków")]
        [Display(Name = "Potwierdź operacje hasłem")]
        public string Password { get; set; }
    }
}