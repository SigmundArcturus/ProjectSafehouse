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
        }

        public Models.User createNewUser(string emailAddress, string unhashedPassword)
        {
            Models.User newUser = new Models.User() { 
                ID = Guid.NewGuid(),
                Email = emailAddress,
                Password = unhashedPassword
            };

            _fakeUsersList.Add(newUser);

            return newUser;
        }

        public Models.User loadUserById(Guid userId)
        {
            Models.User locatedUser = _fakeUsersList.FirstOrDefault(x => x.ID == userId);
            return locatedUser;
        }

        public Models.User loadUserByEmail(string userEmail)
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
            Models.User loaded = loadUserByEmail(emailAddress);
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


        public Models.Company createNewCompany(Models.User creator, string name, string description)
        {
            Models.Company company = new Models.Company()
            {
                Administrators = new List<Models.User>(),
                AllowableStorage = new List<Models.StorageAllocation>(),
                BillableItems = new List<Models.BillingType>(),
                CreatedBy = creator,
                CreatedDate = DateTime.UtcNow,
                Description = description,
                ID = Guid.NewGuid(),
                Name = name,
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
    }
}