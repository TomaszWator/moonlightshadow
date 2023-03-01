using System;
using System.ComponentModel.DataAnnotations;

namespace MoonlightShadow.ViewModels
{
    public class PaymentViewModel
    {
        public decimal FullPrice { get; set; }
        
        public string TitleTransaction { get; set; }

        [Display(Name = "Imie:")]
        [Required(ErrorMessage = "Wprowadź imię")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko:")]
        [Required(ErrorMessage = "Wprowadź nazwisko")]
        public string LastName { get; set; }

        [Display(Name = "Email do wysyłki:")]
        [Required(ErrorMessage = "Wprowadź email do wysyłki")]
        [EmailAddress(ErrorMessage = "Wprowadzono niepoprawny email")]
        public string EmailShipping { get; set; }

        [Display(Name = "Email kontaktowy:")]
        [Required(ErrorMessage = "Wprowadź email kontaktowy")]
        [EmailAddress(ErrorMessage = "Wprowadzono niepoprawny email")]
        public string EmailContact { get; set; }

        [Display(Name = "Numer telefonu:")]
        [Required(ErrorMessage = "Wprowadź numer telefonu")]
        [RegularExpression(@"^(([\+][(][0-9]{2}[)]|[\+]?[0-9]{2})?[\s]?(([0-9]{3}[-\s]?[0-9]{3}[-\s]?[0-9]{3})|(([(][0-9]{2}[)]|[0-9]{2})?[\s]?[\d]{2}[-\s]?([\d]{2}[-\s]?[\d]{3}|[\d]{3}[-\s]?[\d]{2}))))$", 
            ErrorMessage = "Numer telefonu nie jest poprawny")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Kraj:")]
        [Required(ErrorMessage = "Wprowadź kraj")]
        public string Country { get; set; }

        [Display(Name = "Województwo:")]
        [Required(ErrorMessage = "Wprowadź województwo")]
        public string State { get; set; }
        
        [Display(Name = "Miejscowość:")]
        [Required(ErrorMessage = "Wprowadź miejscowość")]
        public string Town { get; set; }

        [Display(Name = "Ulica:")]
        [Required(ErrorMessage = "Wprowadź ulicę")]
        public string Street { get; set; }

        [Display(Name = "Kod pocztowy:")]
        [Required(ErrorMessage = "Wprowadź kod pocztowy")]
        public string ZipCode { get; set; }
    }
}