using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ProjectSafehouse.Abstractions;
using ProjectSafehouse.Models;


namespace ProjectSafehouse.Controllers
{
    public class LoginController : UserAwareController
    {
        //
        // GET: /Login/
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            //return View();
            if (CurrentUser == null)
                return View();
            else
            {
                ViewBag.CurrentUser = CurrentUser;
                return RedirectToAction("Welcome");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult NewUser(User user)
        {
            CurrentUser = DAL.createNewUser(user);
            return RedirectToAction("Welcome");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ReturningUser(User user)
        {
            ViewBag.Error = null;

            CurrentUser = DAL.checkPassword(user.Email, user.Password);

            if (CurrentUser != null)
                return RedirectToAction("Welcome");
            else
            {
                ViewBag.Error = "Login error.  Please check your email and password.";
                return View("Index");
            }
        }

        [HttpGet]
        public ActionResult Welcome()
        {
            return View(CurrentUser);
        }

        [AllowAnonymous]
        public ViewResult LogOut()
        {
            ViewBag.Error = null;
            CurrentUser = null;
            return View("Index");
        }
    }
}
