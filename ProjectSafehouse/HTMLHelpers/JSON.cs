using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace ProjectSafehouse.HTMLHelpers
{
    public static class JSONHelper
    {
        public static string ObjectToJSON(object o)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            string returnMe = ser.Serialize(o);
            return returnMe;
        }
    }
}