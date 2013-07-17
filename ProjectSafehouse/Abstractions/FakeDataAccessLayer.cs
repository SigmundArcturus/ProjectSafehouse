using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSafehouse.Abstractions
{
    public class FakeDataAccessLayer: IDataAccessLayer
    {
        private List<Models.User> _fakeUsersList;
        private List<Models.Company> _fakeCompaniesList;

        public FakeDataAccessLayer()
        {
            _fakeUsersList = new List<Models.User>();
            _fakeCompaniesList = new List<Models.Company>();
        }

        public Models.User createNewUser(Models.User toCreate)
        {
            Models.User newUser = new Models.User() { 
                ID = Guid.NewGuid(),
                Email = toCreate.Email,
                Password = toCreate.Password
            };

            _fakeUsersList.Add(newUser);

            return newUser;
        }

        public Models.User loadUserById(Guid userId, bool includeCompanies)
        {
            Models.User locatedUser = _fakeUsersList.FirstOrDefault(x => x.ID == userId);
            return locatedUser;
        }

        public Models.User loadUserByEmail(string userEmail, bool includeCompanies)
        {
            Models.User locatedUser = _fakeUsersList.FirstOrDefault(x => x.Email == userEmail);
            return locatedUser;
        }

        public IEnumerable<Models.User> findUsers(string searchDetails)
        {
            //throw new NotImplementedException();
            List<Models.User> foundUsers = _fakeUsersList.Where(x => x.Email.Contains(searchDetails)).ToList();
            return foundUsers;
        }

        public Models.User checkPassword(string emailAddress, string unhashedPassword)
        {
            Models.User toCheck = _fakeUsersList.FirstOrDefault(x => x.Email == emailAddress);
            if (toCheck != null && toCheck.Password == unhashedPassword)
            {
                return toCheck;
            }
            else
            {
                return null;
            }
        }

        public bool deleteExistingUser(string emailAddress, string unhashedPassword)
        {
            Models.User loaded = loadUserByEmail(emailAddress, false);
            if (loaded != null)
            {
                _fakeUsersList.Remove(loaded);
                return true;
            }
            else
            {
                return false;
            }
        }

        public string hashPassword(string unhashedPassword)
        {
            return unhashedPassword;
        }


        public Models.Company createNewCompany(Models.User creator, Models.Company toCreate)
        {
            Models.Company company = new Models.Company()
            {
                Administrators = new List<Models.User>(),
                AllowableStorage = new List<Models.StorageAllocation>(),
                BillableItems = new List<Models.BillingType>(),
                CreatedBy = creator,
                CreatedDate = DateTime.UtcNow,
                Description = toCreate.Description,
                ID = Guid.NewGuid(),
                Name = toCreate.Name,
                Projects = new List<Models.Project>(),
                Users = new List<Models.User>()
            };

            company.Administrators.Add(creator);

            _fakeCompaniesList.Add(company);

            return company;
        }

        public bool deleteExistingCompany(Guid creatorID, string unhashedPassword, Guid targetCompanyId)
        {
            Models.Company toRemove = _fakeCompaniesList.FirstOrDefault(x => x.ID == creatorID);

            if (toRemove != null && toRemove.CreatedBy.Password == hashPassword(unhashedPassword))
            {
                _fakeCompaniesList.Remove(toRemove);
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Models.Company> loadUserCompanies(Guid userId, bool includeAdmin, bool includeManager, bool includeUser)
        {
            throw new NotImplementedException();
        }


        public Models.Project createNewProject(Models.User creator, Models.Company company, Models.Project project)
        {
            throw new NotImplementedException();
        }

        public bool deleteExistingProject(Guid creatorID, string unhashedPassword, Guid targetProjectId)
        {
            throw new NotImplementedException();
        }

        public List<Models.Project> loadCompanyProjects(Guid companyId)
        {
            throw new NotImplementedException();
        }


        public Models.Project loadProjectById(Guid projectId)
        {
            throw new NotImplementedException();
        }


        public Models.Release createNewRelease(Models.User creator, Models.Project project, Models.Release toAdd)
        {
            throw new NotImplementedException();
        }

        public bool deleteExistingRelease(Models.User deletedBy, string unhashedPassword, Guid targetReleaseId)
        {
            throw new NotImplementedException();
        }

        public List<Models.Release> loadProjectReleases(Guid projectId)
        {
            throw new NotImplementedException();
        }

        public Models.Release loadReleaseById(Guid releaseId)
        {
            throw new NotImplementedException();
        }

        public List<Models.ActionItem> loadProjectActionItems(Guid projectId)
        {
            throw new NotImplementedException();
        }

        public List<Models.ActionItem> loadReleaseActionItems(Guid releaseId)
        {
            throw new NotImplementedException();
        }


        public Models.Company loadCompanyById(Guid companyId)
        {
            throw new NotImplementedException();
        }


        public Models.ActionItem createNewActionItem(Models.User creator, Models.Release release, Models.ActionItem toCreate, Models.User assignedTo)
        {
            throw new NotImplementedException();
        }


        public Models.Priority loadCompanyActionItemPriority(int number, Guid companyId)
        {
            throw new NotImplementedException();
        }

        public List<Models.ActionItemStatus> loadCompanyActionItemStatuses(Guid companyId)
        {
            throw new NotImplementedException();
        }

        public List<Models.ActionItemType> loadCompanyActionItemTypes(Guid companyId)
        {
            throw new NotImplementedException();
        }


        public bool deleteExistingActionItem(Models.User deletedBy, string unhashedPassword, Guid targetActionItemId)
        {
            throw new NotImplementedException();
        }


        public List<Models.User> loadCompanyUsers(Guid companyId)
        {
            throw new NotImplementedException();
        }


        public Models.ActionItem loadActionItemById(Guid actionItemId)
        {
            throw new NotImplementedException();
        }

        public bool saveChangesToActionItem(Models.ActionItem toUpdate, Models.Release targetRelease, Models.User changedBy)
        {
            throw new NotImplementedException();
        }


        public List<Models.User> loadProjectUsers(Guid projectId)
        {
            throw new NotImplementedException();
        }

        public bool addUserToCompany(Guid companyId, Guid userId, bool isAdmin)
        {
            throw new NotImplementedException();
        }

        public bool addUserToProject(Guid projectId, Guid userId)
        {
            throw new NotImplementedException();
        }


        public List<Models.ActionItemHistoryEvent> loadActionItemHistory(Guid actionItemId)
        {
            throw new NotImplementedException();
        }


        public List<Models.Role> loadUserRoles(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}