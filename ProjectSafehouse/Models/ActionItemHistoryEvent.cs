using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectArsenal.Models
{
    public class ActionItemHistoryEvent
    {
        public Guid ID { get; set; }
        public string WhatChanged { get; set; }
        public string ChangeMade { get; set; }
        public User WhoChangedIt { get; set; }
        public DateTime WhenItChanged { get; set; }
        public Guid Grouping { get; set; }
    }
}