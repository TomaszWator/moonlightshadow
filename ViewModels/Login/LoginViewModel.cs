using System;
using System.ComponentModel.DataAnnotations;

namespace MoonlightShadow.ViewModels
{
    public class LoginViewModel
    {
        public LoginFormViewModel LoginFormViewModel { get; set; }
        public RemindMePasswordViewModel RemindMePasswordViewModel { get; set; }
        public string TypeOfModel { get; set; }
    }
}