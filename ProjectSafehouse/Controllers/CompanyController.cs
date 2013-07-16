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
            List<Models.Company> companies = DAL.loadUserCompanies(userId, true, true, true);

            var returnMe = new ViewModels.CompanyList()
            {
                Companies = companies,
                IntroText = "Get work done for one of these companies:",
                OutroText = "or you could"
            };

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
        public ActionResult Admin(Guid? id)
        {
            Models.Company current = null;
            if (id.HasValue)
            {
                current = DAL.loadCompanyById(id.Value);
            }
            else
            {
                current = CurrentCompany;
            }
            
            if (current != null)
            {
                return View("Admin", current);
            }
            else
            {
                ViewModels.SelectCompany returnMe = new ViewModels.SelectCompany()
                {
                    CurrentUserID = CurrentUser.ID,
                    IntroText = "It looks like you haven't selected a company yet.  Please select a company before trying to access Admin functions.",
                    RedirectTo = ""
                };
                return View("SelectCompany", returnMe);
            }
        }
    }
}
