using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSafehouse.Models
{
    public class StorageAllocation
    {
        public Guid ID { get; set; }
        public double MBSize { get; set; }
        public User CreatedBy { get; set; }
    }
}