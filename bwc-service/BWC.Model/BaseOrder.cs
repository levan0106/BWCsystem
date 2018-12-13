using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Model
{
    public class BaseOrder:Base
    {
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int? Step { get; set; }
        public int? Taxes { get; set; }
        public int? Surcharge { get; set; }
        public double? Discount { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? DatePaid { get; set; }
        public DateTime? FirtReceiveDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string Notes { get; set; }
        public int? OrderType { get; set; }
        public bool NeedToReview { get; set; }
        public double? TotalAmountExcGST { get; set; }
        public double? TotalAmount { get; set; }
        public double? TotalReceived { get; set; }
        public double? TotalPaid { get; set; }
    }
}
