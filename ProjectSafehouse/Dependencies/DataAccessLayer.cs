using System;
using System.Collections.Generic;
using System.Linq;
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
            return dal.createNewUser(emailAddress, unhashedPassword);
        }

        public Models.User loadUserById(Guid userId)
        {
            return dal.loadUserById(userId);
        }

        public Models.User loadUserByEmail(string userEmail)
        {
            return dal.loadUserByEmail(userEmail);
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
    }
}