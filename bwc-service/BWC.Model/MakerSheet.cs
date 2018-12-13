using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Model
{
    public class MakerSheet: Category
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string MaterialName { get; set; }
        public double Width { get; set; }
        public double Drop { get; set; }
        public int Quantity { get; set; }
        public string ColorName { get; set; }
        public string HoodWidth { get; set; }
    }
}
