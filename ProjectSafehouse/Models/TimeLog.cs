using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectArsenal.Models
{
    public class TimeLog
    {
        public Guid ID { get; set; }
        public User LoggedFor { get; set; }
        public User LoggedBy {get; set;}
        public ActionItem Activity { get; set; }
        public DateTime TimeStart { get; set; }
        public FinanceCode ChargeToCode { get; set; }
    }
}