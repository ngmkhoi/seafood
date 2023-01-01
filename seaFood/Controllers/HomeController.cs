using seaFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace seaFood.Controllers
{
    public class HomeController : Controller
    {
        private SeaFoodEntities db = new SeaFoodEntities();
        public ActionResult Index()
        {
            return View(db.products.ToList().Take(10));
        }
    }
}