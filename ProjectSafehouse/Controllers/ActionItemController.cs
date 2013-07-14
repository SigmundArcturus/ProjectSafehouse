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
            List<Models.ActionItemStatus> companyActionItemStatuses = DAL.loadCompanyActionItemStatuses(CurrentCompany.ID);
            List<Models.ActionItemType> companyActionItemTypes = DAL.loadCompanyActionItemTypes(CurrentCompany.ID);
            List<Models.Priority> priorities = CurrentCompany.Priorities;

            //var assignedUsers = toEdit.CurrentActionItem.AssignedTo.Select(x => DAL.loadUserById(x.ID, false));
            var assignedUsers = DAL.loadUserById(toCreate.SelectedUser.ID, false);
            toCreate.CurrentActionItem.AssignedTo = new List<Models.User>() { assignedUsers };//assignedUsers.ToList();

            toCreate.CurrentActionItem.CurrentPriority = priorities.FirstOrDefault(x => x.Order == toCreate.CurrentActionItem.CurrentPriority.Order);
            toCreate.CurrentActionItem.CurrentStatus = companyActionItemStatuses.FirstOrDefault(x => x.ID == toCreate.CurrentActionItem.CurrentStatus.ID);
            toCreate.CurrentActionItem.CurrentType = companyActionItemTypes.FirstOrDefault(x => x.ID == toCreate.CurrentActionItem.CurrentType.ID);

            toCreate.CurrentActionItem.AssignedTo = new List<Models.User>() { DAL.loadUserById(toCreate.SelectedUser.ID, false) };
#warning figure out a way to parse the Estimate based on generic text input like "two hours" or "2h".
            toCreate.CurrentActionItem.Estimate = null;
            toCreate.CurrentActionItem.CreatedBy = CurrentUser;
            toCreate.CurrentActionItem.DateCreated = DateTime.UtcNow;

            Models.Release targetRelease = DAL.loadReleaseById(toCreate.SelectedRelease.ID);

            DAL.createNewActionItem(CurrentUser, targetRelease, toCreate.CurrentActionItem, CurrentUser);

            return RedirectToAction("Overview", "Project");
        }

        [HttpGet]
        public ActionResult Edit(Guid toEditID)
        {
            var toEdit = DAL.loadActionItemById(toEditID);

            List<Models.ActionItemStatus> companyActionItemStatuses = DAL.loadCompanyActionItemStatuses(CurrentCompany.ID);
            List<Models.ActionItemType> companyActionItemTypes = DAL.loadCompanyActionItemTypes(CurrentCompany.ID);

            var assignedSingleUser = toEdit.AssignedTo.FirstOrDefault();

            #warning Convert the below code to load PROJECT-specific users.
            List<ViewModels.SimpleUserInfo> projectUsers = DAL.loadCompanyUsers(CurrentCompany.ID).Select(x => new ViewModels.SimpleUserInfo()
            {
                ID = x.ID,
                Name = x.Name,
                Email = x.Email
            }).ToList();

            List<Models.Priority> priorities = CurrentCompany.Priorities;
            List<Models.Release> releases = DAL.loadProjectReleases(CurrentProject.ID);
            List<Models.ActionItemHistoryEvent> history = DAL.loadActionItemHistory(toEdit.ID);

            ViewModels.EditActionItem toShow = new ViewModels.EditActionItem(toEdit, companyActionItemTypes, companyActionItemStatuses, projectUsers, priorities, releases, history);

            if (assignedSingleUser != null)
                toShow.SelectedUser = projectUsers.FirstOrDefault(x => x.ID == assignedSingleUser.ID) ?? new ViewModels.SimpleUserInfo() { };

            return View(toShow);

        }

        [HttpPost]
        public ActionResult Edit(ViewModels.EditActionItem toEdit)
        {
            List<Models.ActionItemStatus> companyActionItemStatuses = DAL.loadCompanyActionItemStatuses(CurrentCompany.ID);
            List<Models.ActionItemType> companyActionItemTypes = DAL.loadCompanyActionItemTypes(CurrentCompany.ID);
            List<Models.Priority> priorities = CurrentCompany.Priorities;

            //var assignedUsers = toEdit.CurrentActionItem.AssignedTo.Select(x => DAL.loadUserById(x.ID, false));
            var assignedUsers = DAL.loadUserById(toEdit.SelectedUser.ID, false);
            toEdit.CurrentActionItem.AssignedTo = new List<Models.User>() { assignedUsers };//assignedUsers.ToList();

            toEdit.CurrentActionItem.CurrentPriority = priorities.FirstOrDefault(x => x.Order == toEdit.CurrentActionItem.CurrentPriority.Order);
            toEdit.CurrentActionItem.CurrentStatus = companyActionItemStatuses.FirstOrDefault(x => x.ID == toEdit.CurrentActionItem.CurrentStatus.ID);
            toEdit.CurrentActionItem.CurrentType = companyActionItemTypes.FirstOrDefault(x => x.ID == toEdit.CurrentActionItem.CurrentType.ID);

            toEdit.CurrentActionItem.AssignedTo = new List<Models.User>() { DAL.loadUserById(toEdit.SelectedUser.ID, false) };
            toEdit.CurrentActionItem.Estimate = null;
            toEdit.CurrentActionItem.CreatedBy = CurrentUser;
            toEdit.CurrentActionItem.DateCreated = DateTime.UtcNow;

            Models.Release targetRelease = DAL.loadReleaseById(toEdit.SelectedRelease.ID);

            DAL.saveChangesToActionItem(toEdit.CurrentActionItem, targetRelease, CurrentUser);
            return RedirectToAction("Overview", "Project");
        }

    }
}
