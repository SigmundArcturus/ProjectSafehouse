using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectSafehouse.CustomAttributes
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public string roles { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            var currentUser = (Models.User)HttpContext.Current.Session["CurrentUser"];

            if (currentUser != null && !string.IsNullOrWhiteSpace(currentUser.Email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}