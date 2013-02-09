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
        public decimal? HourlyCost { get; set; }
        public decimal? OvertimeMultiplier { get; set; }
        public decimal? OvertimeThreshold { get; set; }
    }
}