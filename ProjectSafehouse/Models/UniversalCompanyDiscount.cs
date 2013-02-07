using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSafehouse.Models
{
    public class UniversalCompanyDiscount
    {
        public Guid ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public double DiscountPercent { get; set; }
        public double DiscountValue { get; set; }
    }
}