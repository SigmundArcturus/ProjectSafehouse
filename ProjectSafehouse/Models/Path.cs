using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSafehouse.Models
{
    public class Path
    {
        public Guid ID { get; set; }
        public User CreatedBy { get; set; }
        public ActionItemType ActionType { get; set; }
        public string Text { get; set; }
        public int Order { get; set; }
        public ActionItemStatus Status {get; set;}
        public List<Step> PathSteps {get; set;}
    }
}