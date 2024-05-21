using RememberMe_Cookies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace RememberMe_Cookies.Controllers
{
    public class LoginController : Controller
    {
        loginEntities db = new loginEntities();
        // GET: Login
        public ActionResult Index()
        {
            HttpCookie cookie = Request.Cookies["user"];
            if (cookie != null)
            {
                ViewBag.username= cookie["username"].ToString();
                string password= cookie["password"].ToString();
                byte[] passwordBytes = Convert.FromBase64String(password);
               string decryptedPassword= ASCIIEncoding.ASCII.GetString(passwordBytes);
                ViewBag.password = decryptedPassword.ToString();
            }

            return View();
        }
        [HttpPost]
        public ActionResult Index(user u)
        {
            HttpCookie cookie = new HttpCookie("user");
            if (ModelState.IsValid == true)
            {
                if (u.rememberMe == true)
                {
                    
                    cookie["username"] = u.username;
                    byte[] b = ASCIIEncoding.ASCII.GetBytes(u.password);
                    String password = Convert.ToBase64String(b);
                    cookie["password"] = password;
                    cookie.Expires = DateTime.Now.AddDays(2);
                    HttpContext.Response.Cookies.Add(cookie);
                }
                else
                {
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    HttpContext.Response.Cookies.Add(cookie);

                }
                var row = db.users.Where(model => model.username == u.username && model.password == u.password).FirstOrDefault();

                if (row != null)
                {
                    Session["username"] = u.username;
                 return   RedirectToAction("Index", "Dashboard");

                }
                else
                {
                    TempData["message"] = "Invalid Credentilas";
                    return View();
                }

            }
            return View();

        }
    }
}