﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectArsenal.Models
{
    public class Release
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public DateTime? StartDate { get; set; }
        public User ScheduledBy { get; set; }
        public List<ActionItem> ActionItems { get; set; }
    }
}