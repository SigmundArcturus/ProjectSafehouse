using System;
using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectArsenal.Abstractions;
using ProjectArsenal.Dependencies;
using System.Linq;
using System.Collections.Generic;

namespace ProjectArsenal.Tests
{
    [TestClass]
    public class ReleaseTesting
    {
        public DataAccessLayer DAL { get; set; }

        public ReleaseTesting()
        {
            UnityConfigurationSection config =
                    ConfigurationManager.GetSection("unity") as UnityConfigurationSection;

            IUnityContainer container = new UnityContainer();
            config.Configure(container);

            DAL = new DataAccessLayer(container.Resolve<IDataAccessLayer>("SQL"));
        }

        [TestMethod]
        public void CanCreateNewRelease()
        {
            // Arrange
            Models.User testUser = DAL.loadUserByEmail("test@test.com", false);
            Models.Company testCompany = DAL.loadUserCompanies(testUser.ID, true, true, true).FirstOrDefault();
            Models.Project testProject = DAL.loadCompanyProjects(testCompany.ID).FirstOrDefault();

            Models.Release testRelease = new Models.Release(){
                ID = Guid.NewGuid(),
                Description = "Test Description",
                Name = "Test Release Name",
                ScheduledBy = testUser,
                ScheduledDate = null,
                StartDate = null
            };

            // Act
            DAL.createNewRelease(testUser, testProject, testRelease);

            // Assert
            List<Models.Release> allReleases = DAL.loadProjectReleases(testProject.ID);
            Assert.IsTrue(allReleases.FirstOrDefault(x => x.ID == testRelease.ID && x.Name == testRelease.Name) != null);
        }
    }
}
