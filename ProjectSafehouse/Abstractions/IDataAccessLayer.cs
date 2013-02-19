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
        User createNewUser(string emailAddress, string unhashedPassword);
        User loadUserById(Guid userId, bool includeCompanies);
        User loadUserByEmail(string userEmail, bool includeCompanies);
        IEnumerable<User> findUsers(string searchDetails);
        User checkPassword(string emailAddress, string unhashedPassword);
        bool deleteExistingUser(string emailAddress, string unhashedPassword);
        string hashPassword(string unhashedPassword);

        // Company methods
        Company createNewCompany(User creator, string name, string description);
        Company loadCompanyById(Guid companyId);
        bool deleteExistingCompany(Guid creatorID, string unhashedPassword, Guid targetCompanyId);
        List<Models.Company> loadUserCompanies(Guid userId, bool includeAdmin, bool includeManager, bool includeUser);

        // Project methods
        Project createNewProject(User creator, Company company, string name, string description);
        bool deleteExistingProject(Guid creatorID, string unhashedPassword, Guid targetProjectId);
        List<Models.Project> loadCompanyProjects(Guid companyId);
        Project loadProjectById(Guid projectId);

        // Release methods
        Models.Release createNewRelease(Models.User creator, Models.Project project, Models.Release toAdd);
        bool deleteExistingRelease(Guid releaseId, string unhashedPassword, Guid targetReleaseId);
        List<Models.Release> loadProjectReleases(Guid projectId);
        Models.Release loadReleaseById(Guid releaseId);

        // ActionItem methods
        Models.ActionItem createNewActionItem(User creator, Release release, ActionItem toCreate, ActionItemStatus startingStatus, User assignedTo);
        List<Models.ActionItem> loadProjectActionItems(Guid projectId);
        List<Models.ActionItem> loadReleaseActionItems(Guid releaseId);


    }
}
