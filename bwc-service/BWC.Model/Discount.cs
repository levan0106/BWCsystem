using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Model
{
    public class Discount:Base
    {
        public string ObjectName { get; set; }
        public int ObjectId { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public double DiscountValue { get; set; }
        public int DiscountType { get; set; }
    }
}
