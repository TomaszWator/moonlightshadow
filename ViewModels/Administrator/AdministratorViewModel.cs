using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MoonlightShadow.Models;
using MoonlightShadow.Models.Transaction;

namespace MoonlightShadow.ViewModels.Administrator
{
    public class AdministratorViewModel
    {
        public List<Transaction> UsersTransactions { get; set; }
        public List<Contact> Contacts { get; set; }
    }
}