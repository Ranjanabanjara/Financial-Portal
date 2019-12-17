using Microsoft.AspNet.Identity;
using Project_4.Enumerations;
using Project_4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Project_4.Enumerations.TransactionType;

namespace Project_4.Extensions
{
    public static class TransactionsExtension
    {
        public static ApplicationDbContext db = new ApplicationDbContext();

        public static void UpdateBalances(this Transaction transaction)
        {
            UpdateBankBalance(transaction);
            UpdateBudgetBalance(transaction);
            UpdateBudgetItemBalance(transaction);

        }

        private static void UpdateBankBalance(Transaction transaction)
        {
            var bank = db.BankAccounts.Find(transaction.BankAccountId);
            if (transaction.TransactionType == TransType.deposit)
                bank.CurrentBalance += transaction.Amount;
            else
                bank.CurrentBalance -= transaction.Amount;
            db.SaveChanges();
        }

        private static void UpdateBudgetBalance(Transaction transaction)
        {
            if (transaction.TransactionType == TransType.deposit || transaction.BudgetItemId == null)
                return;
            var budgetId = db.BudgetItems.Find(transaction.BudgetItemId).BudgetId;
            var budget = db.Budgets.Find(budgetId);
            budget.CurrentAmount += transaction.Amount;
            db.SaveChanges();

            
           
        }
        private static void UpdateBudgetItemBalance(Transaction transaction)
        {
            if (transaction.TransactionType == TransType.deposit || transaction.BudgetItemId == null)
                return;
            var budgetItem = db.BudgetItems.Find(transaction.BudgetItemId);
            budgetItem.CurrentAmount += transaction.Amount;
        
            db.SaveChanges();



        }
        
        public static void ManageNotifications(this Transaction transaction)
        {
            var bankAccount = db.BankAccounts.AsNoTracking().FirstOrDefault(b => b.Id == transaction.BankAccountId);
            var currentBlnc = bankAccount.CurrentBalance;
            if(currentBlnc < 0)
            {
                var body = $"Your recent transaction of amount {transaction.Amount} has over drafted account {bankAccount.Name}";
                var subject = $"Overdraft";
                CreateNotification(transaction,subject, body);
            }

            else if (currentBlnc > 0 && currentBlnc <= bankAccount.LowBalanceThreshold)
            {
                var body = $"Your most recent transaction has triggered a low balance level";
                var subject = $"Low Balance";
                CreateNotification(transaction, subject, body);

            }
           
        }
        private static void CreateNotification(Transaction transaction, string subject, string body)
        {
            var houseId = db.Users.Find(transaction.OwnerId).HouseholdId ?? 0;
            var notification = new Notification
            {
                Body = body,
                Created = DateTime.Now,
                HouseholdId = houseId,
                IsRead = false,
                ReceipentId = transaction.OwnerId,
                Subject = subject
              

            };
            db.Notifications.Add(notification);
            db.SaveChanges();
        }

        public static List<Notification> GetUnreadNotifications()
        {
            var currentUserId = HttpContext.Current.User.Identity.GetUserId();
            return db.Notifications.Where(t => t.ReceipentId == currentUserId && !t.IsRead).ToList();

        }
    }
}