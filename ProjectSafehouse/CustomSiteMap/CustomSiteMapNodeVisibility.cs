using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Extensibility;

namespace ProjectSafehouse.CustomSiteMap
{
    public class CustomSiteMapNodeVisibility: ISiteMapNodeVisibilityProvider
    {
        public bool IsVisible(SiteMapNode node, HttpContext context, IDictionary<string, object> sourceMetadata)
        {
            bool returnMe = false;
            var mvcNode = node as MvcSiteMapNode;
            Models.User user = context.Session["CurrentUser"] as Models.User;
            string visibilitySettings = mvcNode == null ? "" : mvcNode["requireLoggedIn"];
            string roleSettings = mvcNode == null ? "" : mvcNode["customRoles"];
            string[] allowedRoles = (roleSettings ?? "").Split(',');
            List<string> currentRoles = new List<string>();
            if (user != null && user.Roles != null)
                currentRoles = user.Roles.Select(x => x.Name).ToList();

            if (visibilitySettings == "true" && user == null)
            {
                returnMe = false;
            }
            else
            {
                if (allowedRoles.Contains("*"))
                {
                    returnMe = true;
                }
                else if (allowedRoles.Intersect(currentRoles).Any())
                {
                    returnMe = true;
                }
                else
                {
                    returnMe = false;
                }
            }

            return returnMe;
        }
    }
}