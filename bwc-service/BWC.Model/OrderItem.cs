using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Model
{
    public class OrderItem:Base
    {
        //public int OrderItemId { get; set; }
        public object OrderId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
        public double Price { get; set; }
        public string Unit { get; set; }
        public int UnitId { get; set; }
        public int Step { get; set; }
        public string ItemType { get; set; }
        public double TotalAmount { get; set; }
    }
}
