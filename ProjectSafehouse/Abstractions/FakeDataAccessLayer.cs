using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSafehouse.Abstractions
{
    public class FakeDataAccessLayer: IDataAccessLayer
    {
        private List<Models.User> _fakeUsersList;

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

        public Models.User loadUser(Guid userId)
        {
            Models.User locatedUser = _fakeUsersList.FirstOrDefault(x => x.ID == userId);
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
    }
}