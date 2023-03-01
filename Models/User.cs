using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System;
using MoonlightShadow.ViewModels;
using MoonlightShadow.ViewModels.Account;
using Extension.Security;
using Extension.ValidModel;
using MoonlightShadow.Models.Transaction;

namespace MoonlightShadow.Models
{
    public class User
    {
        [BsonId]
        public string Login { get; set; }

        public bool Privileges {get; set; }

        public Password Password { get; set; }

        public Password NewPassword { get; set; }

        public string Email { get; set; }

        public bool IsMailRegisterVerified { get; set; }

        public bool IsPasswordForgotten { get; set; }

        public Order CartOrder { get; set; }

        public Order FollowedOrder { get; set; }

        public Order RecomendedOrder { get; set; }

        public List<BoughtOrder> BoughtOrders { get; set; }

        public string EmailShipping { get; set; }

        public FullName FullName { get; set; }

        public Address Address { get; set; }

        public string PhoneNumber { get; set; }

        public User(SignUpViewModel signUpForm)
        {
            Login = signUpForm.Login;

            Privileges = false;

            Password = new Password(Hasher.Encrypt(signUpForm.Password).ToTuple());

            NewPassword = new Password();

            Email = signUpForm.Email;

            CartOrder = new Order();

            FollowedOrder = new Order();

            RecomendedOrder = new Order();

            BoughtOrders = new List<BoughtOrder>();

            IsMailRegisterVerified = false;

            EmailShipping = "";

            FullName = new FullName();
            FullName.FirstName = "";
            FullName.LastName = "";

            Address = new Address();
            Address.Country = "";
            Address.State = "";
            Address.Town = "";
            Address.Street = "";
            Address.ZipCode = "";

            PhoneNumber = "";
        }

        public void Update(ShippingDataViewModel shippingDataViewModel)
        {
            if (shippingDataViewModel.EmailShipping.IsNotNullOrEmptyOrWhiteSpace())
            {
                EmailShipping = shippingDataViewModel.EmailShipping;
            }

            if (shippingDataViewModel.FirstName.IsNotNullOrEmptyOrWhiteSpace())
            {
                FullName.FirstName = shippingDataViewModel.FirstName;
            }

            if (shippingDataViewModel.LastName.IsNotNullOrEmptyOrWhiteSpace())
            {
                FullName.LastName = shippingDataViewModel.LastName;
            }

            if (shippingDataViewModel.Country.IsNotNullOrEmptyOrWhiteSpace())
            {
                Address.Country = shippingDataViewModel.Country;
            }

            if (shippingDataViewModel.State.IsNotNullOrEmptyOrWhiteSpace())
            {
                Address.State = shippingDataViewModel.State;
            }

            if (shippingDataViewModel.Town.IsNotNullOrEmptyOrWhiteSpace())
            {
                Address.Town = shippingDataViewModel.Town;
            }

            if (shippingDataViewModel.Street.IsNotNullOrEmptyOrWhiteSpace())
            {
                Address.Street = shippingDataViewModel.Street;
            }

            if (shippingDataViewModel.ZipCode.IsNotNullOrEmptyOrWhiteSpace())
            {
                Address.ZipCode = shippingDataViewModel.ZipCode;
            }

            if (shippingDataViewModel.PhoneNumber.IsNotNullOrEmptyOrWhiteSpace())
            {
                PhoneNumber = shippingDataViewModel.PhoneNumber;
            }
        }

        public UserDataViewModel GetUserDataViewModel()
        {
            UserDataViewModel userDataViewModel = new UserDataViewModel();

            userDataViewModel.FullName = new FullName() { FirstName = FullName.FirstName, LastName = FullName.LastName };

            userDataViewModel.PhoneNumber = PhoneNumber;

            userDataViewModel.EmailShipping = EmailShipping;

            userDataViewModel.Address = Address;

            return userDataViewModel;
        }
    
        public ShippingDataViewModel GetShippingDataViewModel()
        {
            var shippingDataViewModel = new ShippingDataViewModel() 
            {
                FirstName = FullName.FirstName,
                LastName = FullName.LastName,
                PhoneNumber = PhoneNumber,
                EmailShipping = EmailShipping,
                Country = Address.Country,
                State = Address.State,
                Town = Address.Town,
                Street = Address.Street,
                ZipCode = Address.ZipCode
            };

            return shippingDataViewModel;
        }
    
        public PaymentViewModel GetPaymentViewModel()
        {
            var paymentViewModel = new PaymentViewModel() 
            {
                FirstName = FullName.FirstName,
                LastName = FullName.LastName,
                PhoneNumber = PhoneNumber,
                EmailShipping = EmailShipping,
                EmailContact = Email,
                Country = Address.Country,
                State = Address.State,
                Town = Address.Town,
                Street = Address.Street,
                ZipCode = Address.ZipCode
            };

            return paymentViewModel;
        }
    }
}