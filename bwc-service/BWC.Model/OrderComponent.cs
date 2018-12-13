using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Model
{
    public class OrderComponent : Base
    {
        public Int64? OrderId { get; set; }
        public int? OrderType { get; set; }
        public int? ComponentId { get; set; }
        public string ComponentName { get; set; }
        public string ComponentCode { get; set; }
        public string Description { get; set; }
        public string ColorName { get; set; }
        public string Color { get; set; }
        public int? ColorId { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public double? ExtendPrice { get; set; }
        public double? Discount { get; set; }
        public int? UnitId { get; set; }
        public string UnitName { get; set; }
        public bool ExtCharge { get; set; }
        public int? Step { get; set; }
        public double? TotalAmount { get; set; }
        public string DeliveryNo { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int? Received { get; set; }
        public int? BackOrder { get; set; }
        public double? AMTExcGST { get; set; }
        public double? GST { get; set; }
        public double? AMTIncGST { get; set; }
        public double? ReceivedAMTExcGST { get; set; }
        public double? ReceivedGST { get; set; }
        public double? ReceivedAMTIncGST { get; set; }
        public string OrderRefNo { get; set; }
        public string Size { get; set; }
    }
}
