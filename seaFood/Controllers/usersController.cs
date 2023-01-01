using System;
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
    public class usersController : Controller
    {
        private SeaFoodEntities db = new SeaFoodEntities();

        public ActionResult SignUp()
        {
            return View();
        }

        // POST: users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp([Bind(Include = "id,name,email,phone,city,address,password,permission")] user user)
        {
            if (ModelState.IsValid)
            {
                var customer = db.users.FirstOrDefault(s => s.name == user.name);
                if (customer != null)
                    ViewBag.Message = "Trùng Tên Đăng Nhập";
                else
                {
                    db.users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("SignIn", "user");
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(user _user)
        {
            if (ModelState.IsValid)
            {
                var admin = db.users.FirstOrDefault(s => s.name == _user.name && s.password == _user.password && s.permission == 1);
                var customer = db.users.FirstOrDefault(s => s.name == _user.name && s.password == _user.password);
                if (admin != null)
                {
                    return RedirectToAction("Index", "homeAdmin", new { area = "admin" });
                }
                else if (customer != null)
                {
                    Session["TaiKhoan"] = customer;
                    return RedirectToAction("Index", "Home");
                }
                else
                    ViewBag.ThongBao = "Tên Đăng Nhập Hoặc Mật Khẩu Không Đúng";
            }
            return View();
        }
        // GET: users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,email,phone,city,address,password,permission")] user user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return Redirect("/");
        }
    }
}

        
       
        
    

