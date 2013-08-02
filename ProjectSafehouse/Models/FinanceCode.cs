using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectArsenal.Models
{
    public class FinanceCode
    {
        public Guid ID { get; set; }
        public string Code { get; set; }
        public User AddedBy { get; set; }
    }
}