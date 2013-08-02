using System;
using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectArsenal.Abstractions;
using ProjectArsenal.Dependencies;
using ProjectArsenal.Models;

namespace ProjectArsenal.Tests
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
            User result = DAL.createNewUser(new User()
            {
                Email = email,
                Password = password
            });
            User find = DAL.loadUserById(result.ID, false);

            //Assert
            Assert.AreEqual(result.ID, find.ID);
            Assert.AreEqual(result.Email, find.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(ProjectArsenal.CustomExceptions.DuplicateUserInsertException))]
        public void CannotCreateDuplicateUser()
        {
            //Arrange
            User toInsert = new User()
            {
                Email = "test@test.com",
                Password = "password"
            };

            //Act
            User result = DAL.createNewUser(toInsert);
            User result1 = DAL.createNewUser(toInsert);

            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ProjectArsenal.CustomExceptions.InvalidUserDataInsertException))]
        public void CannotCreateUserNoEmail()
        {
            //Arrange
            User newUser = new User()
            {
                Email = null,
                Password = "password"
            };

            //Act
            User result = DAL.createNewUser(newUser);

            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ProjectArsenal.CustomExceptions.InvalidUserDataInsertException))]
        public void CannotCreateUserNoPassword()
        {
            //Arrange
            User newUser = new User(){
                Email = "test@test.com",
                Password = null
            };

            //Act
            User result = DAL.createNewUser(newUser);
        }
    }
}
