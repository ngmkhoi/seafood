using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using seaFood.Models;

namespace seaFood.Areas.admin.Controllers
{
    public class order_detailsController : Controller
    {
        private SeaFoodEntities db = new SeaFoodEntities();

        // GET: admin/order_details
        public ActionResult Index()
        {
            var order_details = db.order_details.Include(o => o.order).Include(o => o.product);
            return View(order_details.ToList());
        }

        // GET: admin/order_details/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order_details order_details = db.order_details.Find(id);
            if (order_details == null)
            {
                return HttpNotFound();
            }
            return View(order_details);
        }

        // GET: admin/order_details/Create
        public ActionResult Create()
        {
            ViewBag.order_id = new SelectList(db.orders, "order_id", "name_order");
            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name");
            return View();
        }

        // POST: admin/order_details/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "order_detail_id,order_id,product_id,size,quantity_order,total_price")] order_details order_details)
        {
            if (ModelState.IsValid)
            {
                db.order_details.Add(order_details);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.order_id = new SelectList(db.orders, "order_id", "name_order", order_details.order_id);
            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name", order_details.product_id);
            return View(order_details);
        }

        // GET: admin/order_details/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order_details order_details = db.order_details.Find(id);
            if (order_details == null)
            {
                return HttpNotFound();
            }
            ViewBag.order_id = new SelectList(db.orders, "order_id", "name_order", order_details.order_id);
            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name", order_details.product_id);
            return View(order_details);
        }

        // POST: admin/order_details/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "order_detail_id,order_id,product_id,size,quantity_order,total_price")] order_details order_details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order_details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.order_id = new SelectList(db.orders, "order_id", "name_order", order_details.order_id);
            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name", order_details.product_id);
            return View(order_details);
        }

        // GET: admin/order_details/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order_details order_details = db.order_details.Find(id);
            if (order_details == null)
            {
                return HttpNotFound();
            }
            return View(order_details);
        }

        // POST: admin/order_details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            order_details order_details = db.order_details.Find(id);
            db.order_details.Remove(order_details);
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
