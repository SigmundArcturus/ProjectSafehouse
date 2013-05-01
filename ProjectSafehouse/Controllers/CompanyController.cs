using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectSafehouse.Abstractions;
using ProjectSafehouse.CustomAttributes;

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

        [HttpPost]
        public ActionResult CreateNewCompany(Models.Company toCreate)
        {
            DAL.createNewCompany(CurrentUser, toCreate);
            //List<Models.Company> returnMe = DAL.loadUserCompanies(CurrentUser.ID, true, true, true);
            
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public ActionResult Admin(Guid id)
        {
            Models.Company current = DAL.loadCompanyById(id);
            CurrentCompany = current;

            return View("Admin", current);
        }
    }
}
