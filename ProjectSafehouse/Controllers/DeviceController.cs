using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using ProjectArsenal.Abstractions;
using ProjectArsenal.Dependencies;

namespace ProjectArsenal.Controllers
{
    public class DeviceController : DataAwareController
    {
        [HttpPost]
        public JsonResult BeginDeviceSession(Guid deviceId, Guid appId, string email, string password)
        {
            var returnMe = new Models.DeviceSession();

            var possibleUser = DAL.checkPassword(email, password);
            if (possibleUser != null)
            {
                returnMe.LoadedUser = possibleUser;
            }

            return Json(returnMe);
        }

        [HttpPost]
        public JsonResult EndDeviceSession(Guid sessionId)
        {
            var returnMe = new Models.DeviceSession();

            return Json(returnMe);
        }

    }
}
