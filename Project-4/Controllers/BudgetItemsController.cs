﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_4.Models;

namespace Project_4.Controllers
{
    public class BudgetItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BudgetItems
        public ActionResult Index()
        {
            var budgetItems = db.BudgetItems.Include(b => b.Budget);
            return View(budgetItems.ToList());
        }

        // GET: BudgetItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BudgetItem budgetItem = db.BudgetItems.Find(id);
            if (budgetItem == null)
            {
                return HttpNotFound();
            }
            return View(budgetItem);
        }

        // GET: BudgetItems/Create
        public ActionResult Create()
        {
            ViewBag.BudgetId = new SelectList(db.Budgets, "Id", "OwnerId");
            return View();
        }

        // POST: BudgetItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BudgetId,Name,Description,TargetAmount,CurrentAmount")] BudgetItem budgetItem)
        {

            if (ModelState.IsValid)
            {
                budgetItem.Created = DateTime.Now;
              
                db.BudgetItems.Add(budgetItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BudgetId = new SelectList(db.Budgets, "Id", "OwnerId", budgetItem.BudgetId);
            return View(budgetItem);
        }

        // GET: BudgetItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BudgetItem budgetItem = db.BudgetItems.Find(id);
            if (budgetItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.BudgetId = new SelectList(db.Budgets, "Id", "OwnerId", budgetItem.BudgetId);
            return View(budgetItem);
        }

        // POST: BudgetItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BudgetId,Name,Description,TargetAmount,CurrentAmount,Updated")] BudgetItem budgetItem)
        {
            if (ModelState.IsValid)
            {
                budgetItem.Updated = DateTime.Now;
                db.Entry(budgetItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BudgetId = new SelectList(db.Budgets, "Id", "OwnerId", budgetItem.BudgetId);
            return View(budgetItem);
        }

        // GET: BudgetItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BudgetItem budgetItem = db.BudgetItems.Find(id);
            if (budgetItem == null)
            {
                return HttpNotFound();
            }
            return View(budgetItem);
        }

        // POST: BudgetItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BudgetItem budgetItem = db.BudgetItems.Find(id);
            db.BudgetItems.Remove(budgetItem);
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
