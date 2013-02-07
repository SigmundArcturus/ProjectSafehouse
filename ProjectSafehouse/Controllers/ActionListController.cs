using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectSafehouse.Abstractions;

namespace ProjectSafehouse.Controllers
{
    public class ActionListController : UserAwareController
    {
        //
        // GET: /ActionList/

        public ActionResult Index()
        {
            return View();
        }

    }
}
