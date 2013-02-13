using System;
using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectSafehouse.Abstractions;
using ProjectSafehouse.Dependencies;
using ProjectSafehouse.Models;

namespace ProjectSafehouse.Tests
{
    [TestClass]
    public class UserTesting
    {
        public DataAccessLayer DAL { get; set; }

        public UserTesting()
        {
            UnityConfigurationSection config =
                    ConfigurationManager.GetSection("unity") as UnityConfigurationSection;

            IUnityContainer container = new UnityContainer();
            config.Configure(container);

            DAL = new DataAccessLayer(container.Resolve<IDataAccessLayer>("SQL"));
        }

        [TestMethod]
        public void CanCreateNewUser()
        {
            //Arrange
            string email = "test@test.com";
            string password = "password";

            //Act
            bool test = DAL.deleteExistingUser(email, password);
            User result = DAL.createNewUser(email, password);
            User find = DAL.loadUserById(result.ID, false);

            //Assert
            if (result.ID != find.ID || result.Email != find.Email)
            {
                throw new Exception("Couldn't find user");
            }
        }

        [TestMethod]
        public void CannotCreateDuplicateUser()
        {
            //Arrange
            string email = "test@test.com";
            string password = "password";

            //Act
            User result = DAL.createNewUser(email, password);
            User result1 = DAL.createNewUser(email, password);

            //Assert
        }

        [TestMethod]
        public void CannotCreateUserNoEmail()
        {
            //Arrange
            string email = null;
            string password = "password";

            //Act
            User result = DAL.createNewUser(email, password);

            //Assert
        }

        [TestMethod]
        public void CannotCreateUserNoPassword()
        {
            //Arrange
            string email = "test@test.com";
            string password = null;

            //Act
            User result = DAL.createNewUser(email, password);
        }
    }
}
