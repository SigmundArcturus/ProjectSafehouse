using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectSafehouse.Abstractions;
using ProjectSafehouse.Models;


namespace ProjectSafehouse.Controllers
{
    public class LoginController : UserAwareController
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        public ViewResult NewUser(User user)
        {
            CurrentUser = DAL.createNewUser(user.Email, user.Password);
            return View("Welcome", CurrentUser);
        }

        public ViewResult ReturningUser(User user)
        {
            CurrentUser = DAL.checkPassword(user.Email, user.Password);

            if (CurrentUser != null)
                return View("Welcome", CurrentUser);
            else
                return View("Index");
        }
    }
}
