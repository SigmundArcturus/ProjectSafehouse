using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectSafehouse.Abstractions;
using ProjectSafehouse.ViewModels;

namespace ProjectSafehouse.Controllers
{
    public class UserController : UserAwareController
    {
        [HttpGet]
        public PartialViewResult Partial_AddUserTo(GroupingType groupingType, Guid groupingID)
        {
            List<Models.User> users = new List<Models.User>();
            switch(groupingType){
                case GroupingType.Company:
                    users = DAL.loadCompanyUsers(groupingID);
                    break;
                case GroupingType.Project:
                    users = new List<Models.User>();  // Add this when project users is working.DAL.loadProjectUsers(groupingID);
                    break;
                default:
                    break;
            }

            AddUserTo returnMe = new AddUserTo()
            {
                CurrentUsers = users,
                DestinationID = groupingID,
                DestinationType = groupingType,
                ToAdd = new AddingUser() { Administrator = false, EmailAddress = "" }
            };

            return PartialView("Partial_AddUserTo", returnMe);
        }

        [HttpPost]
        public ActionResult Partial_AddUserTo(AddUserTo AddUserTo)
        {
            Models.User thisUser = DAL.loadUserByEmail(AddUserTo.ToAdd.EmailAddress, false);

            if (thisUser != null)
            {
                switch (AddUserTo.DestinationType)
                {
                    case GroupingType.Company:
                        DAL.addUserToCompany(AddUserTo.DestinationID, thisUser.ID);
                        break;
                    case GroupingType.Project:
                        DAL.addUserToProject(AddUserTo.DestinationID, thisUser.ID);
                        break;
                }
            }

            return RedirectToAction("Admin", "Company", new { id = AddUserTo.DestinationID });
            //return View("~/Company/Admin/" + AddUserTo.DestinationID);
        }

    }
}
