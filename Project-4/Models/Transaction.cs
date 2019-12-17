using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Project_4.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using static Project_4.Enumerations.TransactionType;

namespace Project_4.Models
{
    public class Transaction
    {

        public int Id { get; set; }
        public int BankAccountId { get; set; }
        public string OwnerId { get; set; }
        public int? BudgetItemId { get; set; }  
        public TransType TransactionType { get; set; }
        public double Amount { get; set; }
        public string Memo { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        
        public virtual BudgetItem BudgetItem { get; set; }
       
        public virtual BankAccount BankAccount { get; set; }
     
        public virtual ApplicationUser Owner { get; set; }

    }
}