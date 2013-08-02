using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectArsenal.Models
{
    public class Project
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<Release> ReleaseList { get; set; }
        public List<BillingType> BillableItems { get; set; }
        public List<User> AssignedUsers { get; set; }
        public List<FileFolder> ProjectFolders { get; set; }
        public List<FileRevision> ProjectFiles { get; set; }
    }
}