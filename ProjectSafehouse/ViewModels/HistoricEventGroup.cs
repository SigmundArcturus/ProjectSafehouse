using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSafehouse.ViewModels
{
    public class HistoricEventGroup
    {
        public List<Models.ActionItemHistoryEvent> RelatedEvents { get; set; }
        public Models.User EventCausedBy { get; set; }
        public DateTime EventDateTime { get; set; }
    }
}