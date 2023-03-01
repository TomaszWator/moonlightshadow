using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MoonlightShadow.ViewModels;

namespace MoonlightShadow.Models.Transaction
{
    public class Transaction
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string UserId { get; set; }

        public BoughtOrder BoughtOrder { get; set; }

        public string EmailShipping { get; set; }

        public string EmailContact { get; set; }

        public FullName FullName { get; set; }

        public Address Address { get; set; }

        public string PhoneNumber { get; set; }

        public Transaction(BoughtOrder boughtOrder, PaymentViewModel paymentViewModel, string userId = null)
        {
            UserId = userId;
            BoughtOrder = boughtOrder;
            EmailShipping = paymentViewModel.EmailShipping;
            EmailContact = paymentViewModel.EmailContact;
            FullName = new FullName() 
            { 
                FirstName = paymentViewModel.FirstName,
                LastName = paymentViewModel.LastName 
            };
            Address = new Address() 
            {
                Country = paymentViewModel.Country,
                State = paymentViewModel.State,
                Town = paymentViewModel.Town,
                Street = paymentViewModel.Street,
                ZipCode = paymentViewModel.ZipCode,
            };
            PhoneNumber = paymentViewModel.PhoneNumber;
        }
    }
}