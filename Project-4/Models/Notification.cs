using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_4.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int HouseholdId { get; set; }
        public string ReceipentId { get; set; }
        public DateTime Created { get; set; }
        public string Subject { get; set; }
        public bool IsRead { get; set; }
        public string Body { get; set; }

        public virtual Household Household { get; set; }
        public virtual ApplicationUser Receipent { get; set; }


    }
}
