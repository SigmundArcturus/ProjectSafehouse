using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using Microsoft.Practices.Unity;
using ProjectSafehouse.Abstractions;

namespace ProjectSafehouse.Dependencies
{
    public class DataAccessLayer : IDataAccessLayer
    {
        [Dependency]
        public IDataAccessLayer dal { get; set; }

        public DataAccessLayer(IDataAccessLayer dataLayer)
        {
            dal = dataLayer;
        }

        public Models.User createNewUser(Models.User toCreate)
        {
            // ALWAYS run these validation checks before submitting down to injected DAL.
            // They are necessary in case client-side validation fails.
            //
            if (string.IsNullOrWhiteSpace(toCreate.Email))
                throw new ProjectSafehouse.CustomExceptions.InvalidUserDataInsertException("An empty or null email address was specified for a new user.");

            if (string.IsNullOrWhiteSpace(toCreate.Password))
                throw new ProjectSafehouse.CustomExceptions.InvalidUserDataInsertException("An invalid, null or empty password was specified for a new user: " + toCreate.Email);

            try
            {
                MailAddress m = new MailAddress(toCreate.Email);
            }
            catch (FormatException fex)
            {
                throw new ProjectSafehouse.CustomExceptions.InvalidUserDataInsertException("An invalid email address was specified for a new user: " + toCreate.Email, fex);
            }

            if (loadUserByEmail(toCreate.Email, false) != null)
                throw new ProjectSafehouse.CustomExceptions.DuplicateUserInsertException("A possible duplicate user insert was detected and avoided for email: " + toCreate.Email);

            return dal.createNewUser(toCreate);
        }

        public Models.User loadUserById(Guid userId, bool includeCompanies)
        {
            return dal.loadUserById(userId, includeCompanies);
        }

        public Models.User loadUserByEmail(string userEmail, bool includeCompanies)
        {
            return dal.loadUserByEmail(userEmail, includeCompanies);
        }

        public IEnumerable<Models.User> findUsers(string searchDetails)
        {
            return dal.findUsers(searchDetails);
        }

        public Models.User checkPassword(string emailAddress, string unhashedPassword)
        {
            return dal.checkPassword(emailAddress, unhashedPassword);
        }

        /// <summary>
        /// Delete an existing user from the database.  Returns True if the user existed and was deleted.  Returns False if user didn't exist.
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="unhashedPassword"></param>
        /// <returns>Bool UserExisted</returns>
        public bool deleteExistingUser(string emailAddress, string unhashedPassword)
        {
            return dal.deleteExistingUser(emailAddress, unhashedPassword);
        }

        public string hashPassword(string unhashedPassword)
        {
            return dal.hashPassword(unhashedPassword);
        }

        public Models.Company createNewCompany(Models.User creator, Models.Company toCreate)
        {
            Models.Company createdCompany = dal.createNewCompany(creator, toCreate);

            // always create a default project for any new company
            if (createdCompany != null)
            {

                addUserToCompany(createdCompany.ID, creator.ID);

                List<Models.ActionItemType> companyActionItemTypes = loadCompanyActionItemTypes(createdCompany.ID);
                List<Models.ActionItemStatus> companyActionItemStatuses = loadCompanyActionItemStatuses(createdCompany.ID);

                Models.Project createdProject = dal.createNewProject(creator, createdCompany, new Models.Project(){
                    Name = "Default Project", 
                    Description = "Automatically created for " + toCreate.Name + " during company creation."
                });
                if (createdProject != null)
                {
                    createdCompany.Projects.Add(createdProject);
                    Models.Release createdRelease = createNewRelease(creator, createdProject, new Models.Release()
                    {
                        Description = "This is an empty release to get you started.",
                        Name = "Empty Starting Release",
                        ID = Guid.NewGuid(),
                        ScheduledBy = creator,
                        StartDate = DateTime.UtcNow,
                        ScheduledDate = null
                    });

                    if (createdRelease != null)
                    {
                        createdProject.ReleaseList.Add(createdRelease);
                        Models.ActionItem createdActionItem = createNewActionItem(creator, createdRelease, new Models.ActionItem()
                        {
                            AssignedTo = new List<Models.User>() { creator },
                            CreatedBy = creator,
                            CurrentStatus = companyActionItemStatuses.FirstOrDefault(),
                            CurrentPriority = dal.loadCompanyActionItemPriority(3, createdCompany.ID),
                            CurrentType = companyActionItemTypes.FirstOrDefault(),
                            Description = "Example Action Item for " + createdRelease.Name,
                            Estimate = null,
                            TimeSpent = null,
                            DateCreated = DateTime.UtcNow,
                            DateCompleted = null,
                            ID = Guid.NewGuid(),
                            Title = "Example Action Item",
                            TargetDate = null
                        }, creator);
                    }
                }
            }

            return createdCompany;
        }

        public bool deleteExistingCompany(Guid creatorID, string unhashedPassword, Guid targetCompanyId)
        {
            return dal.deleteExistingCompany(creatorID, unhashedPassword, targetCompanyId);
        }

        public List<Models.Company> loadUserCompanies(Guid userId, bool includeAdmin, bool includeManager, bool includeUser)
        {
            return dal.loadUserCompanies(userId, includeAdmin, includeManager, includeUser);
        }

        public Models.Project createNewProject(Models.User creator, Models.Company company, Models.Project toCreate)
        {
            return dal.createNewProject(creator, company, toCreate);
        }

        public bool deleteExistingProject(Guid creatorID, string unhashedPassword, Guid targetProjectId)
        {
            return dal.deleteExistingProject(creatorID, unhashedPassword, targetProjectId);
        }

        public List<Models.Project> loadCompanyProjects(Guid companyId)
        {
            return dal.loadCompanyProjects(companyId);
        }

        public Models.Project loadProjectById(Guid projectId)
        {
            return dal.loadProjectById(projectId);
        }


        public Models.Release createNewRelease(Models.User creator, Models.Project project, Models.Release toAdd)
        {
            return dal.createNewRelease(creator, project, toAdd);
        }

        public bool deleteExistingRelease(Models.User deletedBy, string unhashedPassword, Guid targetReleaseId)
        {
            return dal.deleteExistingRelease(deletedBy, unhashedPassword, targetReleaseId);
        }

        public List<Models.Release> loadProjectReleases(Guid projectId)
        {
            return dal.loadProjectReleases(projectId);
        }

        public Models.Release loadReleaseById(Guid releaseId)
        {
            return dal.loadReleaseById(releaseId);
        }

        public Models.ActionItem createNewActionItem(Models.User creator, Models.Release release, Models.ActionItem toCreate, Models.User assignedTo)
        {
            return dal.createNewActionItem(creator, release, toCreate, assignedTo);
        }

        public List<Models.ActionItem> loadProjectActionItems(Guid projectId)
        {
            return dal.loadProjectActionItems(projectId);
        }

        public List<Models.ActionItem> loadReleaseActionItems(Guid releaseId)
        {
            return dal.loadReleaseActionItems(releaseId);
        }


        public Models.Company loadCompanyById(Guid companyId)
        {
            return dal.loadCompanyById(companyId);
        }


        public Models.Priority loadCompanyActionItemPriority(int number, Guid companyId)
        {
            return dal.loadCompanyActionItemPriority(number, companyId);
        }

        public List<Models.ActionItemStatus> loadCompanyActionItemStatuses(Guid companyId)
        {
            return dal.loadCompanyActionItemStatuses(companyId);
        }

        public List<Models.ActionItemType> loadCompanyActionItemTypes(Guid companyId)
        {
            return dal.loadCompanyActionItemTypes(companyId);
        }

        public bool deleteExistingActionItem(Models.User deletedBy, string unhashedPassword, Guid targetActionItemId)
        {
            return dal.deleteExistingActionItem(deletedBy, unhashedPassword, targetActionItemId);
        }

        public List<Models.User> loadCompanyUsers(Guid companyId)
        {
            return dal.loadCompanyUsers(companyId);
        }


        public Models.ActionItem loadActionItemById(Guid actionItemId)
        {
            return dal.loadActionItemById(actionItemId);
        }

        public bool saveChangesToActionItem(Models.ActionItem toUpdate, Models.Release targetRelease, Models.User changedBy)
        {
            return dal.saveChangesToActionItem(toUpdate, targetRelease, changedBy);
        }


        public List<Models.User> loadProjectUsers(Guid projectId)
        {
            return dal.loadProjectUsers(projectId);
        }

        public bool addUserToCompany(Guid companyId, Guid userId)
        {
            return dal.addUserToCompany(companyId, userId);
        }

        public bool addUserToProject(Guid projectId, Guid userId)
        {
            return dal.addUserToCompany(projectId, userId);
        }

        public List<Models.ActionItemHistoryEvent> loadActionItemHistory(Guid actionItemId)
        {
            return dal.loadActionItemHistory(actionItemId);
        }
    }
}