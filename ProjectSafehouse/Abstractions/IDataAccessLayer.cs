using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using ProjectSafehouse.Models;

namespace ProjectSafehouse.Abstractions
{
    public interface IDataAccessLayer
    {
        // User methods
        User createNewUser(User toCreate);
        User loadUserById(Guid userId, bool includeCompanies);
        User loadUserByEmail(string userEmail, bool includeCompanies);
        IEnumerable<User> findUsers(string searchDetails);
        User checkPassword(string emailAddress, string unhashedPassword);
        bool deleteExistingUser(string emailAddress, string unhashedPassword);
        string hashPassword(string unhashedPassword);
        List<User> loadCompanyUsers(Guid companyId);
        List<User> loadProjectUsers(Guid projectId);
        bool addUserToCompany(Guid companyId, Guid userId, bool isAdmin);
        bool addUserToProject(Guid projectId, Guid userId);
        List<Role> loadUserRoles(Guid userId);

        // Company methods
        Company createNewCompany(User creator, Company toCreate);
        Company loadCompanyById(Guid companyId);
        bool deleteExistingCompany(Guid creatorID, string unhashedPassword, Guid targetCompanyId);
        List<Models.Company> loadUserCompanies(Guid userId, bool includeAdmin, bool includeManager, bool includeUser);

        // Project methods
        Project createNewProject(User creator, Company company, Project toCreate);
        bool deleteExistingProject(Guid creatorID, string unhashedPassword, Guid targetProjectId);
        List<Models.Project> loadCompanyProjects(Guid companyId);
        Project loadProjectById(Guid projectId);

        // Release methods
        Models.Release createNewRelease(Models.User creator, Models.Project project, Models.Release toAdd);
        bool deleteExistingRelease(Models.User deletedBy, string unhashedPassword, Guid targetReleaseId);
        List<Models.Release> loadProjectReleases(Guid projectId);
        Models.Release loadReleaseById(Guid releaseId);

        // ActionItem methods
        Models.ActionItem createNewActionItem(User creator, Release release, ActionItem toCreate, User assignedTo);
        List<Models.ActionItem> loadProjectActionItems(Guid projectId);
        List<Models.ActionItem> loadReleaseActionItems(Guid releaseId);
        bool deleteExistingActionItem(Models.User deletedBy, string unhashedPassword, Guid targetActionItemId);
        Models.ActionItem loadActionItemById(Guid actionItemId);
        bool saveChangesToActionItem(Models.ActionItem toUpdate, Models.Release targetRelease, Models.User changedBy);

        // ActionItemHistory methods
        List<Models.ActionItemHistoryEvent> loadActionItemHistory(Guid actionItemId);

        // Priority methods
        Models.Priority loadCompanyActionItemPriority(int number, Guid companyId);

        // Status methods
        List<Models.ActionItemStatus> loadCompanyActionItemStatuses(Guid companyId);

        // Type methods
        List<Models.ActionItemType> loadCompanyActionItemTypes(Guid companyId);

        // Device / RESTful methods


    }
}
