using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Model
{
    public class Component:Base
    {
        public string ComponentCode { get; set; }
        public string ComponentName { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Price { get; set; }
        public string PurchasePrice { get; set; }
        public string Color { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
        public string Size { get; set; }
        public int ComponentType { get; set; }
    }
}
