using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Project_4.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Project_4.Enumerations.AccountType;

namespace Project_4.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public int HouseholdId { get; set; }
     
        public AccType  AccountType { get; set; }
        public string OwnerId { get; set; }
        public string Name { get; set; }
       
        public double StartingBalance { get; set; }
        public double CurrentBalance { get; set; }
        public double LowBalanceThreshold { get; set; }
        public DateTime Created { get; set; }

        public virtual Household Household { get; set; }
     
        public virtual ApplicationUser Owner { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
       
        public BankAccount()
        {
            Transactions = new HashSet<Transaction>();

        }

    }
}
