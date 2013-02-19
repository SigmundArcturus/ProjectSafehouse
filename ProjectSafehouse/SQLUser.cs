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
    
    public partial class SQLUser
    {
        public SQLUser()
        {
            this.AdminCompanies = new HashSet<SQLCompany>();
            this.Projects = new HashSet<SQLProject>();
            this.ActionItemUsers = new HashSet<SQLActionItemUser>();
        }
    
        public System.Guid ID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string AvatarURL { get; set; }
        public Nullable<decimal> HourlyCost { get; set; }
        public Nullable<decimal> OvertimeMultiplier { get; set; }
        public Nullable<decimal> OvertimeThreshold { get; set; }
    
        public virtual ICollection<SQLCompany> AdminCompanies { get; set; }
        public virtual ICollection<SQLProject> Projects { get; set; }
        public virtual ICollection<SQLActionItemUser> ActionItemUsers { get; set; }
    }
}
