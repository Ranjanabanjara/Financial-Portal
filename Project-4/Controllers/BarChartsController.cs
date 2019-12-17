using Microsoft.AspNet.Identity;
using Project_4.Helpers;
using Project_4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_4.Controllers
{
    public class BarChartsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private HouseholdHelper householdHelper = new HouseholdHelper();



        public JsonResult BarChartData()
        {
            var userId = User.Identity.GetUserId();
            var household = householdHelper.GetMyHouse();
            var bankAccounts = household.BankAccounts.Where(b => b.OwnerId == userId).ToList();
            var myData = new StackedChart {
                Labels = bankAccounts.Select(b => b.Name).ToList(),
                BarLabel1 = "SB",
                BarLabel2 = "CB",
                BarLabel3 = "LBT",
                BGColor1 = "#11F136",
                BGColor2 = "#EFEB76",
                BGColor3 = "#F13811",
                Data1 = bankAccounts.Select(b => b.StartingBalance).ToList(),
                Data2 = bankAccounts.Select(b => b.CurrentBalance).ToList(),
                Data3 = bankAccounts.Select(b => b.LowBalanceThreshold).ToList()

            };


            return Json(myData);
        }

    }
}

