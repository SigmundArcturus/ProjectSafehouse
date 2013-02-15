using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectSafehouse.Abstractions;

namespace ProjectSafehouse.Controllers
{
    public class ProjectController : UserAwareController
    {
        //
        // GET: /Project/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult LoadCompanyProjects(Guid companyId)
        {
            List<Models.Project> returnMe = DAL.loadCompanyProjects(companyId);

            return PartialView("Partial_ProjectList", returnMe);
        }

        [HttpGet]
        public ViewResult ProjectOverview(Guid projectId)
        {
            Models.Project returnMe = DAL.loadProjectById(projectId);

            return View(returnMe);
        }

    }
}
