using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Project_4.Extensions;
using Project_4.Helpers;
using Project_4.Models;

namespace Project_4.Controllers
{
    public class HouseholdsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private RoleHelper roleHelper = new RoleHelper();

        // GET: Households
        public ActionResult Dashboard()
        {
            return View(db.Households.ToList());
        }
        public ActionResult Setup(int id)
        {
            return View();
        }
       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Setup(WizardViewModel model)
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                //Add and save Bank Account
                model.BankAccount.Created = DateTime.Now;
                model.BankAccount.OwnerId = User.Identity.GetUserId();
                model.BankAccount.HouseholdId = (int)user.HouseholdId;
                db.BankAccounts.Add(model.BankAccount);
                db.SaveChanges();

                //Add and save Budget
                model.Budget.Created = DateTime.Now;
                model.Budget.HouseholdId = (int)user.HouseholdId;
                db.Budgets.Add(model.Budget);
                db.SaveChanges();


                //Add and save BudgetItem
                model.BudgetItem.Created = DateTime.Now;
                model.BudgetItem.BudgetId = model.Budget.Id;
                db.BudgetItems.Add(model.BudgetItem);
                db.SaveChanges();
                return RedirectToAction("Details", "Households", new { id = user.HouseholdId });
            }
            return View(model);

        }

        public async Task<ActionResult> LeaveHousehold()
        {
            var userId = User.Identity.GetUserId();
            var myRole = roleHelper.ListUserRoles(userId).FirstOrDefault();
            var user = db.Users.Find(userId);
            switch (myRole)
            {
                case "HouseholdHead":
                    var members = db.Users.Where(u => u.HouseholdId == user.HouseholdId).Count();
                    if(members > 1)
                    {
                        TempData["Message"] = $"You are the Head of Household and there are total {members} members in this house.";
                        return RedirectToAction("ApointSuccessor");

                    }
                    user.HouseholdId = null;
                    db.SaveChanges();
                    roleHelper.RemoveUserFromRole(userId, "HouseholdHead");
                    await ControllerContext.HttpContext.RefreshAuthentication(user);

                    return RedirectToAction("Dashboard", "Home");

                case "Member":
                default:
                    user.HouseholdId = null;
                    db.SaveChanges();
                    roleHelper.RemoveUserFromRole(userId, "Member");
                    await ControllerContext.HttpContext.RefreshAuthentication(user);
                    return RedirectToAction("Dashboard", "Home");

            }
           
        }

        public ActionResult ApointSuccessor()
        {
            var userId = User.Identity.GetUserId();
            var myHouseholdId = db.Users.Find(userId).HouseholdId ?? 0;
            if (myHouseholdId == 0)
                return RedirectToAction("Dashboard", "Home");
            var members = db.Users.Where(u => u.HouseholdId == myHouseholdId && u.Id != userId);
            ViewBag.newHOH = new SelectList(members, "Id", "FullName");
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ApointSuccessor(string newHOH)
        {
            if (string.IsNullOrEmpty(newHOH))
            
                return RedirectToAction("Dashboard", "Home");

                var me = db.Users.Find(User.Identity.GetUserId());
                me.HouseholdId = null;
                db.SaveChanges();

                roleHelper.RemoveUserFromRole(me.Id, "HouseholdHead");
                await ControllerContext.HttpContext.RefreshAuthentication(me);

                roleHelper.RemoveUserFromRole(newHOH, "Member");
                roleHelper.AddUserToRole(newHOH, "HouseholdHead");
            return RedirectToAction("Dashboard", "Home");
        }
        

           
        
        // GET: Households/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Include(h => h.Owner).FirstOrDefault(h => h.Id == id);
           
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // GET: Households/Create
        public ActionResult Create()
        {

           
            return View();
        }

        // POST: Households/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Greeting")] Household household)
        {
        
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var user = db.Users.Find(userId);
                var userRole = roleHelper.ListUserRoles(userId).FirstOrDefault();
                if (user.HouseholdId != null)
                {
                    ViewBag.Message = "You already have the house";
                    return RedirectToAction("Dashboard", "Home");

                }
                else
                {
                    if (userRole != null)
                    {
                        roleHelper.RemoveUserFromRole(userId, userRole);
                    }
                    if (string.IsNullOrEmpty(userRole))
                    {
                        roleHelper.AddUserToRole(userId, "HouseholdHead");

                    }

                }
              
                household.Created = DateTime.Now;

                db.Households.Add(household);
                db.Users.Find(userId).HouseholdId = household.Id;
                db.SaveChanges();
                await ControllerContext.HttpContext.RefreshAuthentication(db.Users.Find(userId));
                return RedirectToAction("Setup", "Households", new { id = household.Id });
            }

            return View(household);
        }

        // GET: Households/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // POST: Households/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Greeting,Created")] Household household)
        {
            if (ModelState.IsValid)
            {
                db.Entry(household).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(household);
        }

        // GET: Households/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // POST: Households/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Household household = db.Households.Find(id);
            db.Households.Remove(household);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
