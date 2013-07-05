using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSafehouse.Models
{
    public class ActionItemHistoryEvent
    {
        public Guid ID { get; set; }
        public string WhatChanged { get; set; }
        public string ChangeMade { get; set; }
        public User WhoChangedIt { get; set; }
        public DateTime WhenItChanged { get; set; }
    }
}