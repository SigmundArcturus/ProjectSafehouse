//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectSafehouse
{
    using System;
    using System.Collections.Generic;
    
    public partial class SQLCompany
    {
        public SQLCompany()
        {
            this.Projects = new HashSet<SQLProject>();
            this.Priorities = new HashSet<SQLPriority>();
            this.CompanyUsers = new HashSet<SQLCompanyUser>();
        }
    
        public System.Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.Guid CreatedByUserID { get; set; }
        public System.DateTime CreatedDate { get; set; }
    
        public virtual SQLUser User { get; set; }
        public virtual ICollection<SQLProject> Projects { get; set; }
        public virtual ICollection<SQLPriority> Priorities { get; set; }
        public virtual ICollection<SQLCompanyUser> CompanyUsers { get; set; }
    }
}