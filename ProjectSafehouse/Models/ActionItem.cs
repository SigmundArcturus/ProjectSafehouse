using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSafehouse.Models
{
    public class ActionItem
    {
        public Guid ID { get; set; }
        public DateTime DateCreated { get; set; }
        public User CreatedBy { get; set; }
        public List<User> AssignedTo { get; set; }
        public ActionItemStatus CurrentStatus { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ActionItemType CurrentType { get; set; }
        public TimeSpan? Estimate { get; set; }
        public TimeSpan? TimeSpent {get; set;}
        public DateTime? DateCompleted { get; set; }
        public Priority CurrentPriority { get; set; }
        public DateTime? TargetDate { get; set; }
        public IEnumerable<ActionItemHistoryEvent> History { get; set; }
    }
}