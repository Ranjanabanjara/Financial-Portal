using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Project_4.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "First Name")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Firstname must have minimum length of 2 and maximum length of 40")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        public string AvatarPath { get; set; }
        [NotMapped]
        public string NameWithEmail
        {
            get
            {
                return $"{FirstName} {LastName}, {Email}";
            }
        }
        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName}, {LastName}";
            }
        }

        public int? HouseholdId { get; set; }
        public virtual Household Household { get; set; }

        public virtual ICollection<BankAccount> BankAccounts { get; set; }
        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public ApplicationUser()
        {
            Notifications = new HashSet<Notification>();
            Transactions = new HashSet<Transaction>();
            Budgets = new HashSet<Budget>();
            BankAccounts = new HashSet<BankAccount>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Project_4.Models.BankAccount> BankAccounts { get; set; }

        public System.Data.Entity.DbSet<Project_4.Models.Budget> Budgets { get; set; }

        public System.Data.Entity.DbSet<Project_4.Models.Household> Households { get; set; }

        public System.Data.Entity.DbSet<Project_4.Models.BudgetItem> BudgetItems { get; set; }

        public System.Data.Entity.DbSet<Project_4.Models.Invitation> Invitations { get; set; }

        public System.Data.Entity.DbSet<Project_4.Models.Notification> Notifications { get; set; }

        public System.Data.Entity.DbSet<Project_4.Models.Transaction> Transactions { get; set; }
    }
}