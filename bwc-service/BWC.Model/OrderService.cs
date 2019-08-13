using System;

namespace BWC.Model
{
    public class OrderService : Base
    {
        public Int64? OrderId { get; set; }
        public int? OrderType { get; set; }
        public string Task { get; set; }
        public int? Quantity { get; set; }
        public double? Charge { get; set; }
        public DateTime? ServiceDate { get; set; }
        public DateTime? ServiceTime { get; set; }
    }
}
