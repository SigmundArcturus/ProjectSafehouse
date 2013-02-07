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

            DAL = new DataAccessLayer(container.Resolve<IDataAccessLayer>("Fake"));
        }

        [TestMethod]
        public void CreateNewUser()
        {
            //Arrange
            string email = "test@test.com";
            string password = "password";

            //Act
            User result = DAL.createNewUser(email, password);
            User find = DAL.loadUser(result.ID);

            //Assert
            if (result != find)
            {
                throw new Exception("Couldn't find user");
            }
        }
    }
}
