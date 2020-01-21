using Microsoft.AspNet.Identity;
using Project_4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_4.Helpers
{
    public class UserHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationDbContext dbTwo = new ApplicationDbContext();
        public static string GetAvatarPath()
        {
            return db.Users.Find(HttpContext.Current.User.Identity.GetUserId()).AvatarPath;
        }
        public string GetAvatarPathTwo()
        {
          return dbTwo.Users.Find(HttpContext.Current.User.Identity.GetUserId()).AvatarPath;
        }
        public string GetUserFirstNameTwo()
        {
            return dbTwo.Users.Find(HttpContext.Current.User.Identity.GetUserId()).FirstName;
        }
        public static string GetUserEmail()
        {
            return db.Users.Find(HttpContext.Current.User.Identity.GetUserId()).UserName;
        }
        public static string GetUserFirstName()
        {
            return db.Users.Find(HttpContext.Current.User.Identity.GetUserId()).FirstName;
        }
        public static string GetUserLastName()
        {
            return db.Users.Find(HttpContext.Current.User.Identity.GetUserId()).LastName;
        }
        public static ApplicationUser GetUserId()
        {
            var userId = db.Users.Find(HttpContext.Current.User.Identity.GetUserId());
            return userId;

        }
      
    }
}
