using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Model
{
    public class ProductComponent:Base
    {
        public int ProductId { get; set; }
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public string ComponentCode { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public int ColorId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public bool ExtCharge { get; set; }
    }
}
