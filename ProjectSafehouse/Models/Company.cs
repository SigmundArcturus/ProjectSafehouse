using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSafehouse.Models
{
    public class Company
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<BillingType> BillableItems { get; set; }
        public List<StorageAllocation> AllowableStorage { get; set; }
        public List<User> Administrators { get; set; }
        public List<Project> Projects { get; set; }
        public List<User> Users { get; set; }
    }
}