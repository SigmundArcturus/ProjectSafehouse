using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectArsenal.Abstractions;

namespace ProjectArsenal.Controllers
{
    public class ProjectController : UserAwareController
    {
        //
        // GET: /Project/

        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        public PartialViewResult LoadCompanyProjects(Guid companyId)
        {
            CurrentCompany = DAL.loadCompanyById(companyId);
            List<Models.Project> returnMe = DAL.loadCompanyProjects(companyId);

            return PartialView("Partial_ProjectList", returnMe);
        }

        [HttpGet]
        public ViewResult Overview(Guid? projectId)
        {
            Guid? targetId = projectId;

            if (!targetId.HasValue && CurrentProject != null)
                targetId = CurrentProject.ID;

            if (targetId.HasValue)
            {
                Models.Project returnMe = DAL.loadProjectById(targetId.Value);
                CurrentProject = returnMe;
                return View(returnMe);
            }
            else
            {
                return View();
            }
            
        }

    }
}
