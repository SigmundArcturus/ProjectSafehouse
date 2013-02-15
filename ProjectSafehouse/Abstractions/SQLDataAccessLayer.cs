using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Security;

namespace ProjectSafehouse.Abstractions
{
    public class SQLDataAccessLayer: IDataAccessLayer
    {
        private ProjectSafehouseEntities db { get; set; }

        public SQLDataAccessLayer()
        {
            db = new ProjectSafehouseEntities();
        }

        public Models.User createNewUser(string emailAddress, string unhashedPassword)
        {
            string hashedPassword = hashPassword(unhashedPassword);

            Models.User newUser = new Models.User()
            {
                ID = Guid.NewGuid(),
                Email = emailAddress,
                Password = hashedPassword,
                Companies = new List<Models.Company>()
            };

            db.SQLUsers.Add(new SQLUser()
            {
                ID = newUser.ID,
                Password = newUser.Password,
                Email = newUser.Email
            });

            db.SaveChanges();

            return newUser;
        }

        public Models.User loadUserById(Guid userId, bool includeCompanies)
        {
            SQLUser foundUser = db.SQLUsers.FirstOrDefault(x => x.ID == userId);
            return loadUserBySqlUser(foundUser, includeCompanies);
        }

        public Models.User loadUserByEmail(string userEmail, bool includeCompanies)
        {
            SQLUser foundUser = db.SQLUsers.FirstOrDefault(x => x.Email == userEmail);
            return loadUserBySqlUser(foundUser, includeCompanies);            
        }

        private Models.User loadUserBySqlUser(SQLUser foundUser, bool includeCompanies)
        {
            if (foundUser != null)
            {
                Models.User loadedUser = new Models.User()
                {
                    ID = foundUser.ID,
                    AvatarURL = foundUser.AvatarURL,
                    Email = foundUser.Email,
                    HourlyCost = foundUser.HourlyCost,
                    Password = "",
                    Name = foundUser.Name,
                    OvertimeMultiplier = foundUser.OvertimeMultiplier,
                    OvertimeThreshold = foundUser.OvertimeThreshold,
                    Companies = includeCompanies ? loadUserCompanies(foundUser.ID, true, true, true) : null
                };

                return loadedUser;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Models.User> findUsers(string searchDetails)
        {
            throw new NotImplementedException();
        }

        public Models.User checkPassword(string emailAddress, string unhashedPassword)
        {
            string hashedPassword = hashPassword(unhashedPassword);

            SQLUser foundUser = db.SQLUsers.FirstOrDefault(x => x.Email == emailAddress);
            if (foundUser != null && foundUser.Password == hashedPassword)
            {
                Models.User loadedUser = loadUserById(foundUser.ID, false);
                //Models.User loadedUser = new Models.User()
                //{
                //    AvatarURL = foundUser.AvatarURL,
                //    Email = foundUser.Email,
                //    HourlyCost = foundUser.HourlyCost,
                //    ID = foundUser.ID,
                //    Name = foundUser.Name,
                //    OvertimeThreshold = foundUser.OvertimeThreshold,
                //    OvertimeMultiplier = foundUser.OvertimeMultiplier,
                //    Password = "",
                //    Companies = new List<Models.Company>()
                //};

                //foreach (var comp in foundUser.AdminCompanies)
                //{
                //    Models.Company toAdd = new Models.Company()
                //    {
                //        Administrators = new List<Models.User>(),
                //        AllowableStorage = new List<Models.StorageAllocation>(),
                //        BillableItems = new List<Models.BillingType>(),
                //        CreatedBy = null,
                //        CreatedDate = comp.CreatedDate,
                //        Description = comp.Description,
                //        ID = comp.ID,
                //        Name = comp.Name,
                //        Projects = new List<Models.Project>(),
                //        Users = new List<Models.User>()
                //    };

                //    toAdd.CreatedBy = loadedUser;
                //    loadedUser.Companies.Add(toAdd);
                //}

                return loadedUser;
            }
            else
            {
                return null;
            }
        }

        public bool deleteExistingUser(string emailAddress, string unhashedPassword)
        {
            string hashedPassword = hashPassword(unhashedPassword);
            SQLUser foundUser = db.SQLUsers.FirstOrDefault(x => x.Email == emailAddress);
            if (foundUser != null && foundUser.Password == hashedPassword)
            {
                List<Models.Company> userCompanies = loadUserCompanies(foundUser.ID, true, true, true);
                foreach (var company in userCompanies)
                {
                    deleteExistingCompany(foundUser.ID, unhashedPassword, company.ID);
                }
                db.SQLUsers.Remove(foundUser);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        //todo:  implement advanced password protection.
        public string hashPassword(string unhashedPassword)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(unhashedPassword, "sha1"); ;
        }

        public Models.Company createNewCompany(Models.User creator, string name, string description)
        {
            Models.Company newCompany = new Models.Company()
            {
                Administrators = new List<Models.User>(),
                AllowableStorage = new List<Models.StorageAllocation>(),
                BillableItems = new List<Models.BillingType>(),
                CreatedBy = creator,
                CreatedDate = DateTime.UtcNow,
                Description = description,
                Name = name,
                ID = Guid.NewGuid(),
                Projects = new List<Models.Project>(),
                Users = new List<Models.User>()
            };

            SQLCompany newSqlCompany = new SQLCompany()
            {
                Name = newCompany.Name,
                CreatedByUserID = creator.ID,
                CreatedDate = newCompany.CreatedDate,
                Description = newCompany.Description,
                ID = newCompany.ID
            };

            db.SQLCompanies.Add(newSqlCompany);

            db.SaveChanges();

            return newCompany;
        }


        public bool deleteExistingCompany(Guid creatorID, string unhashedPassword, Guid targetCompanyId)
        {
            SQLCompany foundCompany = db.SQLCompanies.FirstOrDefault(x => x.ID == targetCompanyId);
            bool removedACompany = false;

            if (foundCompany != null)
            {
                Models.User foundUser = loadUserById(foundCompany.CreatedByUserID, false);
                if (foundUser != null && checkPassword(foundUser.Email, unhashedPassword) != null)
                {

                    List<Models.Project> deleteProjects = loadCompanyProjects(targetCompanyId);
                    if (deleteProjects.Count(x => x.CreatedBy.ID != creatorID) > 0)
                        throw new Exception("Cannot delete company, it still has projects that you don't own underneath it.");
                    else
                    {
                        foreach (var toDelete in deleteProjects)
                        {
                            deleteExistingProject(creatorID, unhashedPassword, toDelete.ID);
                        }
                    }

                    db.SQLCompanies.Remove(foundCompany);
                    db.SaveChanges();
                    removedACompany = true;
                }
            }

            return removedACompany;
        }

        public List<Models.Company> loadUserCompanies(Guid userId, bool includeAdmin, bool includeManager, bool includeUser)
        {
            List<SQLCompany> foundCompanies = new List<SQLCompany>();
            List<Models.Company> returnMe = new List<Models.Company>();
            if (includeAdmin)
                foundCompanies.AddRange(db.SQLCompanies.Where(x => x.CreatedByUserID == userId));

            foreach (SQLCompany comp in foundCompanies)
            {
                returnMe.Add(new Models.Company()
                {
                    Administrators = new List<Models.User>(),
                    AllowableStorage = new List<Models.StorageAllocation>(),
                    BillableItems = new List<Models.BillingType>(),
                    CreatedBy = loadUserById(comp.CreatedByUserID, false),
                    CreatedDate = comp.CreatedDate,
                    Description = comp.Description,
                    ID = comp.ID,
                    Name = comp.Name,
                    Projects = new List<Models.Project>(),
                    Users = new List<Models.User>()
                });
            };

            return returnMe;
        }


        public Models.Project createNewProject(Models.User creator, Models.Company company, string name, string description)
        {
            Models.Project newProject = new Models.Project()
            {
                ID = Guid.NewGuid(),
                Name = name,
                Description = description,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = creator,
                AssignedUsers = new List<Models.User>(),
                BillableItems = new List<Models.BillingType>(),
                ProjectFiles = new List<Models.FileRevision>(),
                ProjectFolders = new List<Models.FileFolder>(),
                ReleaseList = new List<Models.Release>()
            };

            SQLProject sqlProject = new SQLProject()
            {
                CompanyId = company.ID,
                CreatedByUserId = creator.ID,
                CreatedDate = newProject.CreatedDate,
                Description = description,
                Name = name,
                ID = newProject.ID
            };

            db.SQLProjects.Add(sqlProject);

            db.SaveChanges();

            return newProject;
        }

        public bool deleteExistingProject(Guid creatorID, string unhashedPassword, Guid targetProjectId)
        {
            SQLProject foundProject = db.SQLProjects.FirstOrDefault(x => x.ID == targetProjectId);
            bool removedAProject = false;

            if (foundProject != null)
            {
                Models.User foundUser = loadUserById(foundProject.CreatedByUserId, false);
                if (foundUser != null && checkPassword(foundUser.Email, unhashedPassword) != null)
                {
                    db.SQLProjects.Remove(foundProject);
                    db.SaveChanges();
                    removedAProject = true;
                }
            }

            return removedAProject;
        }

        public List<Models.Project> loadCompanyProjects(Guid companyId)
        {
            List<SQLProject> foundSQLProjects = db.SQLProjects.Where(x => x.CompanyId == companyId).ToList();


            List<Models.Project> foundProjects = foundSQLProjects.Select(
                 y => new Models.Project()
                 {
                     AssignedUsers = new List<Models.User>(),
                     BillableItems = new List<Models.BillingType>(),
                     CreatedBy = loadUserById(y.CreatedByUserId, false),
                     CreatedDate = y.CreatedDate,
                     Description = y.Description,
                     ID = y.ID,
                     Name = y.Name,
                     ProjectFiles = new List<Models.FileRevision>(),
                     ProjectFolders = new List<Models.FileFolder>(),
                     ReleaseList = new List<Models.Release>()
                 }
                ).ToList();

            return foundProjects;
        }
    }
}