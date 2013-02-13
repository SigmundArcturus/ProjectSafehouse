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
        User loadUserById(Guid userId, bool includeCompanies);
        User loadUserByEmail(string userEmail, bool includeCompanies);
        IEnumerable<User> findUsers(string searchDetails);
        User checkPassword(string emailAddress, string unhashedPassword);
        bool deleteExistingUser(string emailAddress, string unhashedPassword);
        string hashPassword(string unhashedPassword);
        Company createNewCompany(User creator, string name, string description);
        bool deleteExistingCompany(Guid creatorID, string unhashedPassword, Guid targetCompanyId);
        List<Models.Company> loadUserCompanies(Guid userId, bool includeAdmin, bool includeManager, bool includeUser);
    }
}
