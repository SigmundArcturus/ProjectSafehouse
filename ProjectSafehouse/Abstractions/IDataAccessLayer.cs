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
        User createNewUser(string emailAddress, string unhashedPassword);
        User loadUser(Guid userId);
        IEnumerable<User> findUsers(string searchDetails);
        User checkPassword(string emailAddress, string unhashedPassword);
    }
}
