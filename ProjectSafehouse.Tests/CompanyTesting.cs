using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectSafehouse.Abstractions;
using ProjectSafehouse.Dependencies;

namespace ProjectSafehouse.Tests
{
    [TestClass]
    public class CompanyTesting
    {
        public DataAccessLayer DAL { get; set; }

        public CompanyTesting()
        {
            UnityConfigurationSection config =
                    ConfigurationManager.GetSection("unity") as UnityConfigurationSection;

            IUnityContainer container = new UnityContainer();
            config.Configure(container);

            DAL = new DataAccessLayer(container.Resolve<IDataAccessLayer>("SQL"));
        }

        [TestMethod]
        public void CanCreateNewCompany()
        {
            // Arrange
            Models.User testUser = DAL.loadUserByEmail("test@test.com");
            List<Models.Company> userCompanies = DAL.loadUserCompanies(testUser.ID, true, true, true);
            foreach(var userCompany in userCompanies)
            {
                bool removedTestCompany = DAL.deleteExistingCompany(testUser.ID, "password", userCompany.ID);
            }

            // Act
            Models.Company testCompany = DAL.createNewCompany(testUser, "Test Company", "A company created by the test methods.");

            // Assert

        }
    }
}
