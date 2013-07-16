using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSafehouse.ViewModels
{
    public class CompanyList
    {
        public string IntroText { get; set; }
        public string OutroText { get; set; }
        public List<Models.Company> Companies { get; set; }
    }
}