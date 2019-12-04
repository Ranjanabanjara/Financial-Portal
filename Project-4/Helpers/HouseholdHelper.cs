using Microsoft.AspNet.Identity;
using Project_4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_4.Helpers
{
    public class HouseholdHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();

      
        public ICollection<ApplicationUser> ListMembersOnHouse(int houseId)
        {

            
            return db.Households.Find(houseId).Owner;
        }
        



    }
}