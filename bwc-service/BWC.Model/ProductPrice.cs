using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Model
{
    public class ProductPrice:Base
    {
        public Int64 GroupId { get; set; }
        public int ProductId { get; set; }
        public int MaterialId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public double Value { get; set; }
        public int IsActive { get; set; }
        public int? PriceType { get; set; }

    }
}
