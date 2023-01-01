﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using seaFood.Models;

namespace seaFood.Controllers
{
    public class subcategoriesController : Controller
    {
        private SeaFoodEntities db = new SeaFoodEntities();

        // GET: subcategories
        public ActionResult Index()
        {
            return View(db.subcategories.ToList());
        }

        // GET: subcategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subcategory subcategory = db.subcategories.Find(id);
            if (subcategory == null)
            {
                return HttpNotFound();
            }
            return View(subcategory);
        }

        // GET: subcategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: subcategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "subcategory_id,subcategory_name")] subcategory subcategory)
        {
            if (ModelState.IsValid)
            {
                db.subcategories.Add(subcategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subcategory);
        }

        // GET: subcategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subcategory subcategory = db.subcategories.Find(id);
            if (subcategory == null)
            {
                return HttpNotFound();
            }
            return View(subcategory);
        }

        // POST: subcategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "subcategory_id,subcategory_name")] subcategory subcategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subcategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subcategory);
        }

        // GET: subcategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subcategory subcategory = db.subcategories.Find(id);
            if (subcategory == null)
            {
                return HttpNotFound();
            }
            return View(subcategory);
        }

        // POST: subcategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            subcategory subcategory = db.subcategories.Find(id);
            db.subcategories.Remove(subcategory);
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
