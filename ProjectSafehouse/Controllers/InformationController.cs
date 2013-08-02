using ProjectArsenal.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectArsenal.Controllers
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
