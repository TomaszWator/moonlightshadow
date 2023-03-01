using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MoonlightShadow.Models;
using MoonlightShadow.Models.Transaction;

namespace MoonlightShadow.ViewModels.Administrator
{
    public class AdministratorViewModel
    {
        public IEnumerable<Transaction> PaymentTransactions { get; set; }
        public IEnumerable<Transaction> ShippmentTransactions { get; set; }
    }
}