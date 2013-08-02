using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectArsenal.Models
{
    public class Administrator
    {
        public Guid ID { get; set; }
        public Company Company {get; set;}
        public User User { get; set; }
        public User PromotedBy { get; set; }
    }
}