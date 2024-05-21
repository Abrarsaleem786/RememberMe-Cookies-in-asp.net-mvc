using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RememberMe_Cookies.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
               return RedirectToAction("Index", "Login");

               
            }
            else
            {
                return View();
            }

            return View();
        }
        public ActionResult Logout()
        {
            if (Session["username"] != null)
            {
                Session.Abandon();
               
            }
            return RedirectToAction("Index", "Login");

        }
    }
}