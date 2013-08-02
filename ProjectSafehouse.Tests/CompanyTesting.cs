using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectArsenal.Abstractions;
using ProjectArsenal.Dependencies;

namespace ProjectArsenal.Tests
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
            Models.User testUser = DAL.loadUserByEmail("test@test.com", false);
            List<Models.Company> userCompanies = DAL.loadUserCompanies(testUser.ID, true, true, true);
            foreach(var userCompany in userCompanies)
            {
                bool removedTestCompany = DAL.deleteExistingCompany(testUser.ID, "password", userCompany.ID);
            }

            // Act
            Models.Company testCompany = DAL.createNewCompany(testUser, new Models.Company(){
                Name="Test Company", 
                Description = "A company created by the test methods."
            });

            // Assert
            List<Models.Company> companies = DAL.loadUserCompanies(testUser.ID, true, true, true);
            Assert.IsTrue(companies.Find(x => x.Name == "Test Company" && x.Description == "A company created by the test methods.") != null);
        }
    }
}
