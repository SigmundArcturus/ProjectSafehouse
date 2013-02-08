using System;
using System.Collections.Generic;
using System.Linq;
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
            string hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(unhashedPassword, "sha1");

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

        public Models.User loadUser(Guid userId)
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

        public IEnumerable<Models.User> findUsers(string searchDetails)
        {
            throw new NotImplementedException();
        }

        public Models.User checkPassword(string emailAddress, string unhashedPassword)
        {
            string hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(unhashedPassword, "sha1");

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
    }
}