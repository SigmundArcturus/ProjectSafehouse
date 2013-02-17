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

        public Models.User createNewUser(string emailAddress, string unhashedPassword)
        {
            // ALWAYS run these validation checks before submitting down to injected DAL.
            // They are necessary in case client-side validation fails.
            //
            if (string.IsNullOrWhiteSpace(emailAddress))
                throw new ProjectSafehouse.CustomExceptions.InvalidUserDataInsertException("An empty or null email address was specified for a new user.");

            if (string.IsNullOrWhiteSpace(unhashedPassword))
                throw new ProjectSafehouse.CustomExceptions.InvalidUserDataInsertException("An invalid, null or empty password was specified for a new user: " + emailAddress);

            try
            {
                MailAddress m = new MailAddress(emailAddress);
            }
            catch (FormatException fex)
            {
                throw new ProjectSafehouse.CustomExceptions.InvalidUserDataInsertException("An invalid email address was specified for a new user: " + emailAddress, fex);
            }

            if (loadUserByEmail(emailAddress, false) != null)
                throw new ProjectSafehouse.CustomExceptions.DuplicateUserInsertException("A possible duplicate user insert was detected and avoided for email: " + emailAddress);

            return dal.createNewUser(emailAddress, unhashedPassword);
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

        public Models.Company createNewCompany(Models.User creator, string name, string description)
        {
            Models.Company createdCompany = dal.createNewCompany(creator, name, description);

            // always create a default project for any new company
            if (createdCompany != null)
            {
                Models.Project createdProject = dal.createNewProject(creator, createdCompany, "Default Project", "Automatically created for " + name + " during company creation.");
                if (createdProject != null)
                    createdCompany.Projects.Add(createdProject);
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

        public Models.Project createNewProject(Models.User creator, Models.Company company, string name, string description)
        {
            return dal.createNewProject(creator, company, name, description);
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

        public bool deleteExistingRelease(Guid releaseId, string unhashedPassword, Guid targetReleaseId)
        {
            return dal.deleteExistingRelease(releaseId, unhashedPassword, targetReleaseId);
        }

        public List<Models.Release> loadProjectReleases(Guid projectId)
        {
            return dal.loadProjectReleases(projectId);
        }

        public Models.Release loadReleaseById(Guid releaseId)
        {
            return dal.loadReleaseById(releaseId);
        }

        public Models.ActionItem createNewActionItem(Models.User creator, Models.Release release, string name, string description, Models.ActionItemStatus startingStatus, Models.User assignedTo)
        {
            return dal.createNewActionItem(creator, release, name, description, startingStatus, assignedTo);
        }

        public List<Models.ActionItem> loadProjectActionItems(Guid projectId)
        {
            return dal.loadProjectActionItems(projectId);
        }

        public List<Models.ActionItem> loadReleaseActionItems(Guid releaseId)
        {
            return dal.loadReleaseActionItems(releaseId);
        }
    }
}