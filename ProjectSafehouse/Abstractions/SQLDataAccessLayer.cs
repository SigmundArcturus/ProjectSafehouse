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

            //run test for email address validation (in case client-side validation is circumvented)
            try
            {
                MailAddress m = new MailAddress(emailAddress);
            }
            catch (FormatException)
            {
                return null;
            }

            Models.User newUser = new Models.User()
            {
                ID = Guid.NewGuid(),
                Email = emailAddress,
                Password = hashedPassword
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

        public Models.User loadUserById(Guid userId)
        {
            SQLUser foundUser = db.SQLUsers.FirstOrDefault(x => x.ID == userId);

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
                    OvertimeThreshold = foundUser.OvertimeThreshold
                };

                return loadedUser;
            }
            else
            {
                return null;
            }
        }

        public Models.User loadUserByEmail(string userEmail)
        {
            SQLUser foundUser = db.SQLUsers.FirstOrDefault(x => x.Email == userEmail);

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
                    OvertimeThreshold = foundUser.OvertimeThreshold
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
                Models.User loadedUser = new Models.User()
                {
                    AvatarURL = foundUser.AvatarURL,
                    Email = foundUser.Email,
                    HourlyCost = foundUser.HourlyCost,
                    ID = foundUser.ID,
                    Name = foundUser.Name,
                    OvertimeThreshold = foundUser.OvertimeThreshold,
                    OvertimeMultiplier = foundUser.OvertimeMultiplier,
                    Password = ""
                };

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
                Models.User foundUser = loadUserById(foundCompany.CreatedByUserID);
                if (foundUser != null && checkPassword(foundUser.Email, unhashedPassword) != null)
                {
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
                    CreatedBy = loadUserById(comp.CreatedByUserID),
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
    }
}