using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace seaFood.Areas.admin.Controllers
{
    // GET: admin/homeAdmin
    public class homeAdminController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
            
        public ActionResult Logout()
        {
            Session.Clear();
            return Redirect("/");
        }
     
    }
}
