using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using seaFood.Models;
using PagedList;
using PagedList.Mvc;
using System.Drawing.Printing;
using System.Web.UI;

namespace seaFood.Controllers
{
    public class productsController : Controller
    {
        private SeaFoodEntities db = new SeaFoodEntities();

        // GET: products
        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;
            {
                var products = db.products.Include(p => p.category).Include(p => p.subcategory).OrderBy(p => p.product_id);
                int pageSize = 4;
                int pageNumber = (page ?? 1);   
                return View(products.ToPagedList(pageNumber, pageSize));
            }            
        }
        // GET: products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: products/Create
        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name");
            ViewBag.subcategory_id = new SelectList(db.subcategories, "subcategory_id", "subcategory_name");
            return View();
        }

        // POST: products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "product_id,product_name,price,images,description,category_id,subcategory_id")] product product, HttpPostedFileBase images)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (images.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(images.FileName);
                        string _path = Path.Combine(Server.MapPath("~/images"), _FileName);
                        images.SaveAs(_path);
                        product.images = _FileName;
                    }
                    db.products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Message = "không thành công!!";
                }
            }

            ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name", product.category_id);
            ViewBag.subcategory_id = new SelectList(db.subcategories, "subcategory_id", "subcategory_name", product.subcategory_id);
            return View(product);
        }

        // GET: products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name", product.category_id);
            ViewBag.subcategory_id = new SelectList(db.subcategories, "subcategory_id", "subcategory_name", product.subcategory_id);
            return View(product);
        }

        // POST: products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "product_id,product_name,price,images,description,category_id,subcategory_id")] product product, HttpPostedFileBase images, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (images != null)
                    {
                        string _FileName = Path.GetFileName(images.FileName);
                        string _path = Path.Combine(Server.MapPath("~/images"), _FileName);
                        images.SaveAs(_path);
                        product.images = _FileName;
                        // get Path of old image for deleting it
                        _path = Path.Combine(Server.MapPath("~/images"), form["oldimage"]);
                        if (System.IO.File.Exists(_path))
                            System.IO.File.Delete(_path);

                    }
                    else
                        product.images = form["oldimage"];
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Message = "không thành công!!";
                }
                return RedirectToAction("Index");
            }
            ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name", product.category_id);
            ViewBag.subcategory_id = new SelectList(db.subcategories, "subcategory_id", "subcategory_name", product.subcategory_id);
            return View(product);
        }

        // GET: products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
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
