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
        private List<Models.Priority> defaultPriorities { get; set; }

        public SQLDataAccessLayer()
        {
            db = new ProjectSafehouseEntities();
            defaultPriorities = new List<Models.Priority>(){ 
                new Models.Priority(){
                    CreatedBy = null,
                    CreatedOn = DateTime.MinValue,
                    Description = "Default High Priority",
                    Name = "High Priority",
                    Order = 1
                },
                new Models.Priority(){
                    CreatedBy = null,
                    CreatedOn = DateTime.MinValue,
                    Description = "Default Medium Priority",
                    Name = "Medium Priority",
                    Order = 2
                },
                new Models.Priority(){
                    CreatedBy = null,
                    CreatedOn = DateTime.MinValue,
                    Description = "Default Low Priority",
                    Name = "Low Priority",
                    Order = 3
                }
            };
        }

        public Models.User createNewUser(Models.User toCreate)
        {
            string hashedPassword = hashPassword(toCreate.Password);

            Models.User newUser = new Models.User()
            {
                ID = Guid.NewGuid(),
                Email = toCreate.Email,
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

        public Models.Company createNewCompany(Models.User creator, Models.Company toCreate)
        {
            Models.Company newCompany = new Models.Company()
            {
                Administrators = new List<Models.User>(),
                AllowableStorage = new List<Models.StorageAllocation>(),
                BillableItems = new List<Models.BillingType>(),
                CreatedBy = creator,
                CreatedDate = DateTime.UtcNow,
                Description = toCreate.Description,
                Name = toCreate.Name,
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


        public Models.Project createNewProject(Models.User creator, Models.Company company, Models.Project toCreate)
        {
            Models.Project newProject = new Models.Project()
            {
                ID = Guid.NewGuid(),
                Name = toCreate.Name,
                Description = toCreate.Description,
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
                Description = newProject.Description,
                Name = newProject.Name,
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

                List<Models.Release> foundReleases = loadProjectReleases(foundProject.ID);

                foreach (var release in foundReleases)
                {
                    deleteExistingRelease(foundUser, unhashedPassword, release.ID);
                }

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


        public Models.Project loadProjectById(Guid projectId)
        {
            SQLProject foundSQLProject = db.SQLProjects.FirstOrDefault(x => x.ID == projectId); 

            Models.Project foundProject = new Models.Project()
            {
                AssignedUsers = new List<Models.User>(),
                BillableItems = new List<Models.BillingType>(),
                CreatedBy = loadUserById(foundSQLProject.CreatedByUserId, false),
                CreatedDate = foundSQLProject.CreatedDate,
                Description = foundSQLProject.Description,
                ID = foundSQLProject.ID,
                Name = foundSQLProject.Name,
                ProjectFiles = new List<Models.FileRevision>(),
                ProjectFolders = new List<Models.FileFolder>(),
                ReleaseList = loadProjectReleases(projectId)
            };

            return foundProject;
        }


        public Models.Release createNewRelease(Models.User creator, Models.Project project, Models.Release toAdd)
        {
            SQLRelease toInsert = new SQLRelease()
            {
                Description = toAdd.Description,
                ID = toAdd.ID,
                Name = toAdd.Name,
                ProjectID = project.ID,
                ScheduledByID = creator.ID,
                ScheduledDate = toAdd.ScheduledDate,
                StartDate = toAdd.StartDate
            };

            toAdd.ScheduledBy = creator;

            db.SQLReleases.Add(toInsert);
            db.SaveChanges();

            return toAdd;
        }

        public bool deleteExistingRelease(Models.User deletedBy, string unhashedPassword, Guid targetReleaseId)
        {
            SQLRelease foundRelease = db.SQLReleases.FirstOrDefault(x => x.ID == targetReleaseId);
            bool removedAProject = false;

            if (foundRelease != null)
            {
                List<Models.ActionItem> foundActionItems = loadReleaseActionItems(targetReleaseId);

                foreach (Models.ActionItem actionItem in foundActionItems)
                {
                    var removedActionItem = deleteExistingActionItem(deletedBy, unhashedPassword, actionItem.ID);
                }

                if (deletedBy != null && checkPassword(deletedBy.Email, unhashedPassword) != null)
                {
                    db.SQLReleases.Remove(foundRelease);
                    db.SaveChanges();
                    removedAProject = true;
                }
            }

            return removedAProject;
        }

        public List<Models.Release> loadProjectReleases(Guid projectId)
        {
            List<Models.Release> projectReleases = new List<Models.Release>();

            List<SQLRelease> found = db.SQLReleases.Where(x => x.ProjectID == projectId).ToList();

            projectReleases = found.Select(x => new Models.Release(){
                ID = x.ID,
                Description = x.Description,
                Name = x.Name,
                ScheduledDate = x.ScheduledDate,
                StartDate = x.StartDate,
                ScheduledBy = loadUserById(x.ScheduledByID, false),
                ActionItems = loadReleaseActionItems(x.ID)
            }).ToList();

            return projectReleases;
        }

        public Models.Release loadReleaseById(Guid releaseId)
        {
            Models.Release returnMe = null;
            SQLRelease found = db.SQLReleases.FirstOrDefault(x => x.ID == releaseId);
            if (found != null)
            {
                returnMe = new Models.Release()
                {
                    ID = found.ID,
                    Description = found.Description,
                    Name = found.Name,
                    ScheduledBy = loadUserById(found.ID, false),
                    StartDate = found.StartDate,
                    ScheduledDate = found.ScheduledDate,
                    ActionItems = loadReleaseActionItems(releaseId)
                };
            }
            return returnMe;
        }

        public Models.ActionItem createNewActionItem(Models.Release release, Models.ActionItem toCreate)
        {
            SQLActionItem toSave = new SQLActionItem()
            {
                ActionItemTypeId = toCreate.CurrentType.ID,
                CreatedByUserId = toCreate.CreatedBy.ID,
                InReleaseId = release.ID,
                CurrentPriority = toCreate.CurrentPriority.Order,
                CurrentStatusId = toCreate.CurrentStatus.ID,
                DateCompleted = toCreate.DateCompleted,
                DateCreated = toCreate.DateCreated,
                Description = toCreate.Description,
                ID = toCreate.ID == Guid.Empty ? Guid.NewGuid() : toCreate.ID,
                Name = toCreate.Title,
                IndividualTargetDate = toCreate.TargetDate,
                Estimate = null,
                TimeSpent = null
            };

            if (toCreate.Estimate.HasValue)
                toSave.Estimate = toCreate.Estimate.Value.Ticks;

            if (toCreate.TimeSpent.HasValue)
                toSave.TimeSpent = toCreate.TimeSpent.Value.Ticks;

            db.SQLActionItems.Add(toSave);
            db.SaveChanges();

            return toCreate;
        }

        private List<Models.User> loadModelUsersFromSQLUsers(List<SQLUser> fromDB, bool includeCompanies)
        {
            List<Models.User> toReturn = new List<Models.User>();

            toReturn = fromDB.Select(x => new Models.User()
            {
                AvatarURL = x.AvatarURL,
                Companies = includeCompanies ? loadUserCompanies(x.ID, true, true, true) : new List<Models.Company>(),
                Email = x.Email,
                HourlyCost = x.HourlyCost,
                ID = x.ID,
                Name = x.Name,
                OvertimeMultiplier = x.OvertimeMultiplier,
                OvertimeThreshold = x.OvertimeThreshold,
                Password = null
            }).ToList();

            return toReturn;
        }

        private List<Models.Priority> loadPriorityListForCompanyById(Guid companyId)
        {
            List<Models.Priority> returnMe = new List<Models.Priority>();

            List<SQLPriority> foundPriorities = db.SQLPriorities.Where(x => x.CompanyId == companyId).ToList();
            if (foundPriorities.Count > 0)
            {
                returnMe = foundPriorities.Select(x => new Models.Priority()
                {
                    Description = x.Description,
                    CreatedBy = null,
                    CreatedOn = DateTime.UtcNow,
                    Name = x.Name,
                    Order = x.Number
                }).ToList();
            }
            else
            {
                returnMe = defaultPriorities;
            }
            
            return returnMe;
        }

        public List<Models.ActionItem> loadProjectActionItems(Guid projectId)
        {
            List<Models.ActionItem> toReturn = new List<Models.ActionItem>();

            List<SQLActionItem> fromDB = db.SQLActionItems.Where(x => x.Release.ProjectID == projectId).ToList();
            
            SQLProject project = db.SQLProjects.FirstOrDefault(x => x.ID == projectId);

            List<Models.Priority> companyPriorities = loadPriorityListForCompanyById(project.CompanyId);

            toReturn = fromDB.Select(y => new Models.ActionItem()
            {
                AssignedTo = loadModelUsersFromSQLUsers(y.ActionItemUsers.Select(x => x.User).ToList(), false),
                CreatedBy = loadUserById(y.ID, false),
                CurrentStatus = new Models.ActionItemStatus()
                {
                    Description = y.Status.Description,
                    Name = y.Status.Name,
                    ID = y.Status.ID
                },
                CurrentPriority = companyPriorities.FirstOrDefault(z => z.Order == y.CurrentPriority),
                CurrentType = new Models.ActionItemType()
                {
                    Description = y.ActionItemType.Description,
                    ID = y.ActionItemType.ID,
                    Title = y.ActionItemType.Name
                },
                Description = y.Description,
                DateCreated = y.DateCreated,
                DateCompleted = y.DateCompleted,
                ID = y.ID,
                Estimate = y.Estimate.HasValue ? TimeSpan.FromTicks(y.Estimate.Value) : new TimeSpan(),
                TimeSpent = y.TimeSpent.HasValue ? TimeSpan.FromTicks(y.TimeSpent.Value) : new TimeSpan(),
                TargetDate = y.IndividualTargetDate ?? y.Release.ScheduledDate,
                Title = y.Name
            }).ToList();

            return toReturn;
        }

        public List<Models.ActionItem> loadReleaseActionItems(Guid releaseId)
        {
            List<Models.ActionItem> toReturn = new List<Models.ActionItem>();

            List<SQLActionItem> fromDB = db.SQLActionItems.Where(x => x.InReleaseId == releaseId).ToList();

            SQLRelease release = db.SQLReleases.FirstOrDefault(x => x.ID == releaseId);

            List<Models.Priority> companyPriorities = loadPriorityListForCompanyById(release.Project.CompanyId);

            toReturn = fromDB.Select(y => new Models.ActionItem()
            {
                AssignedTo = loadModelUsersFromSQLUsers(y.ActionItemUsers.Select(x => x.User).ToList(), false),
                CreatedBy = loadUserById(y.ID, false),
                CurrentStatus = new Models.ActionItemStatus()
                {
                    Description = y.Status.Description,
                    Name = y.Status.Name,
                    ID = y.Status.ID
                },
                CurrentPriority = companyPriorities.FirstOrDefault(z => z.Order == y.CurrentPriority),
                CurrentType = new Models.ActionItemType()
                {
                    Description = y.ActionItemType.Description,
                    ID = y.ActionItemType.ID,
                    Title = y.ActionItemType.Name
                },
                Description = y.Description,
                DateCreated = y.DateCreated,
                DateCompleted = y.DateCompleted,
                ID = y.ID,
                Estimate = y.Estimate.HasValue ? TimeSpan.FromTicks(y.Estimate.Value) : new TimeSpan(),
                TimeSpent = y.TimeSpent.HasValue ? TimeSpan.FromTicks(y.TimeSpent.Value) : new TimeSpan(),
                TargetDate = y.IndividualTargetDate ?? y.Release.ScheduledDate,
                Title = y.Name
            }).ToList();

            return toReturn;
        }


        public Models.Company loadCompanyById(Guid companyId)
        {
            SQLCompany fromDb = db.SQLCompanies.FirstOrDefault(x => x.ID == companyId);

            Models.User companyCreator = loadUserById(fromDb.User.ID, false);

            Models.Company returnMe = new Models.Company()
            {
                CreatedBy = companyCreator,
                Administrators = new List<Models.User>(){ companyCreator },
                AllowableStorage = new List<Models.StorageAllocation>(),
                BillableItems = new List<Models.BillingType>(),
                CreatedDate = fromDb.CreatedDate,
                Description = fromDb.Description,
                ID = fromDb.ID,
                Name = fromDb.Name,
                Priorities = loadPriorityListForCompanyById(fromDb.ID),
                Projects = loadCompanyProjects(fromDb.ID),
                Users = new List<Models.User>() {  companyCreator }
            };

            return returnMe;
        }

        public Models.ActionItem createNewActionItem(Models.User creator, Models.Release release, Models.ActionItem toCreate, Models.User assignedTo)
        {
            toCreate.AssignedTo = new List<Models.User>(){ assignedTo };

            SQLActionItem toInsert = new SQLActionItem()
            {
                ActionItemTypeId = toCreate.CurrentType.ID,
                CreatedByUserId = toCreate.CreatedBy.ID,
                CurrentPriority = toCreate.CurrentPriority.Order,
                CurrentStatusId = toCreate.CurrentStatus.ID,
                DateCompleted = toCreate.DateCompleted,
                DateCreated = toCreate.DateCreated,
                Description = toCreate.Description,
                ID = toCreate.ID == Guid.Empty ? Guid.NewGuid() : toCreate.ID,
                Estimate = null,
                InReleaseId = release.ID,
                Name = toCreate.Title,
                TimeSpent = null
            };

            if (toCreate.Estimate.HasValue)
                toInsert.Estimate = toCreate.Estimate.Value.Ticks;

            if (toCreate.TimeSpent.HasValue)
                toInsert.TimeSpent = toCreate.TimeSpent.Value.Ticks;

            db.SQLActionItems.Add(toInsert);
            db.SaveChanges();

            return toCreate;
        }


        public Models.Priority loadCompanyActionItemPriority(int number, Guid companyId)
        {
            Models.Priority returnMe = new Models.Priority();
            var found = db.SQLPriorities.FirstOrDefault(x => x.CompanyId == companyId && x.Number == number);

            if (found != null)
            {
                returnMe.Name = found.Name;
                returnMe.Description = found.Description;
                returnMe.CreatedOn = DateTime.UtcNow;
                returnMe.CreatedBy = null;
                returnMe.Order = found.Number;
            }
            else if (defaultPriorities.Count(x => x.Order == number) > 0)
            {
                returnMe = defaultPriorities.FirstOrDefault(x => x.Order == number);
            }
            else
            {
                returnMe.Name = string.Format("Priority {0}", number);
                returnMe.Description = "";
                returnMe.Order = number;
                returnMe.CreatedOn = DateTime.UtcNow;
                returnMe.CreatedBy = null;
            }

            return returnMe;
        }

        public List<Models.ActionItemStatus> loadCompanyActionItemStatuses(Guid companyId)
        {
            List<SQLStatus> found = db.SQLStatuses.Where(x => x.CompanyId == companyId).ToList();

            // if the company doesn't have any statuses setup, then use the defaults.
            if (found.Count == 0)
                found = db.SQLStatuses.Where(x => x.CompanyId == null).ToList();

            List<Models.ActionItemStatus> returnMe = found.Select(x => new Models.ActionItemStatus()
            {
                Description = x.Description,
                Name = x.Name,
                ID = x.ID
            }).ToList();

            return returnMe;
        }

        public List<Models.ActionItemType> loadCompanyActionItemTypes(Guid companyId)
        {
            List<SQLActionItemType> found = db.SQLActionItemTypes.Where(x => x.CompanyId == companyId).ToList();

            // if the company doesn't have any Action Item Types setup, then load the defaults.
            if (found.Count == 0)
                found = db.SQLActionItemTypes.Where(x => x.CompanyId == null).ToList();

            List<Models.ActionItemType> returnMe = found.Select(x => new Models.ActionItemType()
            {
                Description = x.Description,
                Title = x.Name,
                ID = x.ID
            }).ToList();

            return returnMe;
        }

        public bool deleteExistingActionItem(Models.User deletedBy, string unhashedPassword, Guid targetActionItemId)
        {
            bool deletedSomething = false;

            if (checkPassword(deletedBy.Email, unhashedPassword) != null)
            {
                var toDelete = db.SQLActionItems.FirstOrDefault(x => x.ID == targetActionItemId);
                if (toDelete != null)
                {
                    db.SQLActionItems.Remove(toDelete);
                    deletedSomething = true;
                }
            }

            return deletedSomething;
        }


        public List<Models.User> loadCompanyUsers(Guid companyId)
        {
            List<Models.User> returnMe = new List<Models.User>();

            var targetCompany = db.SQLCompanies.FirstOrDefault(x => x.ID == companyId);
            if (targetCompany != null)
            {
                // do actual work here
                #warning loadCompanyUsers in SQLDataAccessLayer does not actually limit by company.  IMPLEMENT!
            }

            returnMe = loadModelUsersFromSQLUsers(db.SQLUsers.ToList(), false);

            return returnMe;
        }
    }
}