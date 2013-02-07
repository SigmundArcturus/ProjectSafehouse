using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSafehouse.Models
{
    public class User
    {
        public Guid ID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string AvatarURL { get; set; }
        public double HourlyCost { get; set; }
        public double OvertimeMultiplier { get; set; }
        public double OvertimeThreshold { get; set; }
    }
}