using System;
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
        public void CreateNewCompany()
        {
            // Arrange
            Models.User testUser = DAL.loadUserByEmail("test@test.com");

            // Act
            Models.Company testCompany = DAL.createNewCompany(testUser, "Test Company", "A company created by the test methods.");

            // Assert

        }
    }
}
