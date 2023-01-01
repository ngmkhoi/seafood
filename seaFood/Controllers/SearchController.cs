using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using seaFood.Models;

namespace seaFood.Controllers
{
    public class SearchController : Controller
    {
        SeaFoodEntities db = new SeaFoodEntities();
        // GET: Search
        public ActionResult Index(string search)
        {
            var products = db.products.Include(p => p.category).Include(p => p.subcategory);
            if (!String.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                products = products.Where(b => b.product_name.ToLower().Contains(search));
            }
            return View(products.ToList());
        }
    }
}