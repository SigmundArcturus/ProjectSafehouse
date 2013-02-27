using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectSafehouse.Abstractions;

namespace ProjectSafehouse.Controllers
{
    public class ActionItemController : UserAwareController
    {
        //
        // GET: /ActionList/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            List<Models.ActionItemStatus> companyActionItemStatuses = DAL.loadCompanyActionItemStatuses(CurrentCompany.ID);
            List<Models.ActionItemType> companyActionItemTypes = DAL.loadCompanyActionItemTypes(CurrentCompany.ID);

            #warning Convert the below code to load PROJECT-specific users.
            List<ViewModels.SimpleUserInfo> projectUsers = DAL.loadCompanyUsers(CurrentCompany.ID).Select(x => new ViewModels.SimpleUserInfo(){
                ID = x.ID,
                Name = x.Name,
                Email = x.Email
            }).ToList();

            List<Models.Priority> priorities = CurrentCompany.Priorities;
            List<Models.Release> releases = DAL.loadProjectReleases(CurrentProject.ID);

            return View(new ViewModels.CreateActionItem(companyActionItemTypes, companyActionItemStatuses, projectUsers, priorities, releases));
        }

        [HttpPost]
        public ActionResult Create(ViewModels.CreateActionItem toCreate)
        {
            toCreate.CurrentActionItem.AssignedTo = new List<Models.User>() { DAL.loadUserById(toCreate.SelectedUser.ID, false) };
            toCreate.CurrentActionItem.Estimate = null;
            toCreate.CurrentActionItem.CreatedBy = CurrentUser;
            toCreate.CurrentActionItem.DateCreated = DateTime.UtcNow;

            Models.Release targetRelease = DAL.loadReleaseById(toCreate.SelectedRelease.ID);

            DAL.createNewActionItem(CurrentUser, targetRelease, toCreate.CurrentActionItem, CurrentUser);

            return RedirectToAction("ProjectOverview", "Project");
        }

        [HttpGet]
        public ActionResult Edit(Models.ActionItem toEdit)
        {
            return View(toEdit);
        }

        //[HttpPost]
        //public ActionResult Edit(Models.ActionItem toEdit)
        //{
        //    return View("../Project/ProjectOverview", CurrentProject);
        //}

    }
}
