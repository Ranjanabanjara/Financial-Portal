using Microsoft.AspNet.Identity;
using Project_4.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project_4.Helpers
{
    public class HouseholdHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private RoleHelper roleHelper = new RoleHelper();


        public ICollection<ApplicationUser> ListMembersOnHouse(int houseId)
        {
            return db.Households.Find(houseId).Owner;
        }

        public bool IsUserOnHouse(string userId, int houseId)
        {
            var house = db.Households.Find(houseId);
            var flag = house.Owner.Any(o => o.Id == userId);
            return (flag);
        }
        public void RemoveMemberFromHouse(string userId, int houseId)
        {
            if (IsUserOnHouse(userId, houseId))
            {
                Household house = db.Households.Find(houseId);
                var delUser = db.Users.Find(userId);

                house.Owner.Remove(delUser);
                db.Entry(house).State = EntityState.Modified; // just saves this obj instance.
                db.SaveChanges();
            }
        }
        public  Household GetMyHouse()
        {
           
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            var myHouse = db.Households.Where(h => h.Id == user.HouseholdId).FirstOrDefault();           
            return myHouse;
        }
        public List<BankAccount> ListMyBanks()
        {
            var myBanks = new List<BankAccount>();
            var userId = HttpContext.Current.User.Identity.GetUserId();
            myBanks.AddRange(db.BankAccounts.Where(b => b.OwnerId == userId));
            return myBanks;
        }

        public List<Budget> ListMyBudgets()
        {
            var myBudget = new List<Budget>();
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            var myRole = roleHelper.ListUserRoles(userId).FirstOrDefault();
            myBudget.AddRange(db.Budgets.Where(b => b.OwnerId == userId));
            return myBudget;
        }

        public List<Transaction> ListMyTransactions()
        {
            var transactions = new List<Transaction>();
            var userId = HttpContext.Current.User.Identity.GetUserId();                     
            transactions.AddRange(db.Transactions.Where(t => t.OwnerId == userId));                 
            return transactions;
        }
        public List<Transaction> ListHouseholdTransactions()
        {
            var transactions = new List<Transaction>();
            var userId = HttpContext.Current.User.Identity.GetUserId();
             var user = db.Users.Find(userId);
             var household = user.Household;
             transactions = household.BankAccounts.SelectMany(b => b.Transactions).ToList();
             return transactions;
        }

     public List<ApplicationUser> GetOnlymembers()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var myHouseholdId = db.Users.Find(userId).HouseholdId;           
            var members = db.Users.Where(u => u.HouseholdId == myHouseholdId && u.Id != userId).ToList();
            return members;
        }
    

    }
}