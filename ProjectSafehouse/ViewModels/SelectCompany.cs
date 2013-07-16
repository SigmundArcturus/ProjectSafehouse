using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSafehouse.ViewModels
{
    public class SelectCompany
    {
        public string RedirectTo { get; set; }
        public Guid CurrentUserID { get; set; }
        public string IntroText { get; set; }
    }
}