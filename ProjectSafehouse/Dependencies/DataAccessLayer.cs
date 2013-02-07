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

        public Models.User loadUser(Guid userId)
        {
            return dal.loadUser(userId);
        }

        public IEnumerable<Models.User> findUsers(string searchDetails)
        {
            return dal.findUsers(searchDetails);
        }

        public Models.User checkPassword(string emailAddress, string unhashedPassword)
        {
            return dal.checkPassword(emailAddress, unhashedPassword);
        }
    }
}