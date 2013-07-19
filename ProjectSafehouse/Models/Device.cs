using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSafehouse.Models
{
    [Serializable]
    public class Device
    {
        public string FriendlyName { get; set; }
        public Guid ID { get; set; }
        public string Description { get; set; }
    }
}