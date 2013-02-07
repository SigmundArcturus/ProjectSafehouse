using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSafehouse.Models
{
    public class HistoricEvent
    {
        public Guid ID { get; set; }
        public DateTime ChangeDate { get; set; }
        public User ChangedBy { get; set; }
        public string Description { get; set; }
    }
}