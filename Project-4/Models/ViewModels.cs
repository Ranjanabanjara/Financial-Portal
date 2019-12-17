using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_4.Models
{
    public class UserProfileViewModel
    {
            public string Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string DisplayName { get; set; }
            public string AvatarPath { get; set; }

     }

    public class WizardViewModel
    {
        public BankAccount BankAccount { get; set; }
        public Budget Budget { get; set; }
        public BudgetItem BudgetItem { get; set; }

        public WizardViewModel()
        {
            BankAccount = new BankAccount();
            Budget = new Budget();
            BudgetItem = new BudgetItem();
        }

    }

    public class AcceptInvitationViewModel : RegisterViewModel
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public int HouseholdId { get; set; }
      

    }

  
}