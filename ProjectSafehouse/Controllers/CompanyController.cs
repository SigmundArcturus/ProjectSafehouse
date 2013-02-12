using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectSafehouse.Abstractions;

namespace ProjectSafehouse.Controllers
{
    public class CompanyController : UserAwareController
    {
        //
        // GET: /Company/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult LoadUserCompanies(Guid userId)
        {
            List<Models.Company> returnMe = DAL.loadUserCompanies(userId, true, true, true);

            return PartialView("Partial_CompanyList", returnMe);
        }

    }
}
