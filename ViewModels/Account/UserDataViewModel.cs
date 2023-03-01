using MoonlightShadow.Models;

namespace MoonlightShadow.ViewModels.Account
{
    public class UserDataViewModel
    {
        public FullName FullName { get; set; }

        public Address Address { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailShipping { get; set; }
    }
}