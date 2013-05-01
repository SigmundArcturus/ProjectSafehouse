using ProjectSafehouse.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectSafehouse.Controllers
{
    public class InformationController : UserAwareController
    {

        [AllowAnonymous]
        public ViewResult FAQ()
        {
            if (CurrentUser == null)
                return View();
            else
            {
                ViewBag.CurrentUser = CurrentUser;
                return View(CurrentUser);
            }
        }

    }
}
