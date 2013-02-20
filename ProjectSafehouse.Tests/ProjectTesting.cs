using System;
using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectSafehouse.Abstractions;
using ProjectSafehouse.Dependencies;
using ProjectSafehouse.Models;
using System.Linq;
using System.Collections.Generic;

namespace ProjectSafehouse.Tests
{
    [TestClass]
    public class ProjectTesting
    {
        public DataAccessLayer DAL { get; set; }

        public ProjectTesting()
        {
            UnityConfigurationSection config =
                    ConfigurationManager.GetSection("unity") as UnityConfigurationSection;

            IUnityContainer container = new UnityContainer();
            config.Configure(container);

            DAL = new DataAccessLayer(container.Resolve<IDataAccessLayer>("SQL"));
        }

        [TestMethod]
        public void CanCreateNewProject()
        {
            //Arrange
            string email = "test@test.com";
            string password = "password";
            string projectName = "Test Project";
            string projectDescription = "Test project created by unit testing.";
            User testUser = DAL.checkPassword(email, password);
            Company testCompany = DAL.loadUserCompanies(testUser.ID, true, true, true).FirstOrDefault();

            //Act
            DAL.createNewProject(testUser, testCompany, 
                new Project(){
                    Name = projectName, 
                    Description = projectDescription
                });   

            //Assert
            List<Project> foundProjects = DAL.loadCompanyProjects(testCompany.ID);
            Assert.IsTrue(foundProjects.FirstOrDefault(x => x.Name == projectName && x.Description == projectDescription && x.CreatedBy.ID == testUser.ID) != null);
        }

        [TestMethod]
        public void CanLoadExistingProjectById()
        {
            //Arrange 
            string email = "test@test.com";
            string password = "password";
            string projectName = "Test Project";
            string projectDescription = "Test project created by unit testing.";
            User testUser = DAL.checkPassword(email, password);
            Company testCompany = DAL.loadUserCompanies(testUser.ID, true, true, true).FirstOrDefault();
            Project firstProject = DAL.loadCompanyProjects(testCompany.ID).FirstOrDefault();

            //Act
            Project loadProject = DAL.loadProjectById(firstProject.ID);
            
            //Assert
            Assert.IsTrue(firstProject.ID == loadProject.ID);
            Assert.IsTrue(firstProject.Name == loadProject.Name);
            Assert.IsTrue(firstProject.Description == loadProject.Description);
            Assert.IsTrue(firstProject.CreatedDate == loadProject.CreatedDate);
        }
    }
}
