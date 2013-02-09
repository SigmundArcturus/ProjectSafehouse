using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using ProjectSafehouse.Dependencies;

namespace ProjectSafehouse.Abstractions
{
    public class UserAwareController : Controller
    {
        public Models.User CurrentUser {
            get
            {
                return (Models.User)HttpContext.Session["CurrentUser"];
            }
            set
            {
                 HttpContext.Session["CurrentUser"] = value;
            }
        }

        public DataAccessLayer DAL { get; set; } 

        public UserAwareController()
        {

            UnityConfigurationSection config =
                    ConfigurationManager.GetSection("unity") as UnityConfigurationSection;

            IUnityContainer container = new UnityContainer();
            config.Configure(container);

            DAL = new DataAccessLayer(container.Resolve<IDataAccessLayer>("SQL"));
        }

    }
}