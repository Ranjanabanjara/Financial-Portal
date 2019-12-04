using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_4.Models
{
    public class AccountType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //
        //collection
        public ICollection<BankAccount> BankAccount { get; set; }
        //
        //constructor
        public AccountType()
        {
            this.BankAccount = new HashSet<BankAccount>();
        }



    }
}