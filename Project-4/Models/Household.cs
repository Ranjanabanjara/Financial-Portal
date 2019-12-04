﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_4.Models
{
    public class Household
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Greeting { get; set; }

        public DateTime Created { get; set; }

        public virtual ICollection <ApplicationUser> Owner { get; set; }
        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<Invitation> Invitations { get; set; }
        public virtual ICollection<BankAccount> BankAccounts { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }

        public Household()
        {
            BankAccounts = new HashSet<BankAccount>();
            Budgets = new HashSet<Budget>();
            Invitations = new HashSet<Invitation>();
            Notifications = new HashSet<Notification>();
            Owner = new HashSet<ApplicationUser>();
        }

    }
}
