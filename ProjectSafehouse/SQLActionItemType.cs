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
    
    public partial class SQLActionItemType
    {
        public SQLActionItemType()
        {
            this.ActionItems = new HashSet<SQLActionItem>();
        }
    
        public System.Guid ID { get; set; }
        public Nullable<System.Guid> CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<SQLActionItem> ActionItems { get; set; }
    }
}
