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
    
    public partial class SQLActionItemHistory
    {
        public System.Guid ID { get; set; }
        public string ThingChanged { get; set; }
        public string DescriptionOfChange { get; set; }
        public System.DateTime ChangedWhen { get; set; }
        public System.Guid ChangedBy { get; set; }
        public System.Guid ActionItemID { get; set; }
        public System.Guid ChangeGrouping { get; set; }
    
        public virtual SQLActionItem ActionItem { get; set; }
        public virtual SQLUser User { get; set; }
    }
}
