using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginBCryptMVCDemo.Data;
using LoginBCryptMVCDemo.Models;

namespace LoginBCryptMVCDemo.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        public ActionResult Index()
        {
            
                return View();
            
        }
        public ActionResult Register()
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                    session.Save(user);
                    txn.Commit();
                    RedirectToAction("Login","Home");
                }
                return View();
            }
        }


        
        public ActionResult Login()
        {
            using(var session = NHibernateHelper.CreateSession())
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var user = session.Query<User>().FirstOrDefault(u => u.UserName == username);

                if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    return RedirectToAction("HomePage", "Home");
                }
                // Handle invalid login
                ViewBag.ErrorMessage = "Invalid username or password. Please try again.";
                return View();

            }

            
        }
        public ActionResult HomePage()
        {
            return View();
        }
    }
}