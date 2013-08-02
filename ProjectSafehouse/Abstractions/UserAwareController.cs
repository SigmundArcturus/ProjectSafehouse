using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using ProjectArsenal.Dependencies;

namespace ProjectArsenal.Abstractions
{
    public class UserAwareController : DataAwareController
    {
        public Models.User CurrentUser {
            get
            {
                return (Models.User)HttpContext.Session["CurrentUser"];
            }
            set
            {
                 HttpContext.Session["CurrentUser"] = value;
                 ViewBag.CurrentUser = value;
            }
        }

        public Models.Company CurrentCompany
        {
            get
            {
                return (Models.Company)HttpContext.Session["CurrentCompany"];
            }
            set
            {
                HttpContext.Session["CurrentCompany"] = value;
                ViewBag.CurrentCompany = value;
            }
        }

        public Models.Project CurrentProject
        {
            get
            {
                return (Models.Project)HttpContext.Session["CurrentProject"];
            }
            set
            {
                HttpContext.Session["CurrentProject"] = value;
                ViewBag.CurrentProject = value;
            }
        }

        public Models.Release CurrentRelease
        {
            get
            {
                return (Models.Release)HttpContext.Session["CurrentRelease"];
            }
            set
            {
                HttpContext.Session["CurrentRelease"] = value;
                ViewBag.CurrentRelease = value;
            }
        }
    }
}