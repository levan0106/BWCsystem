using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Model
{
    public class MakerSheetComponent:Base
    {
        public int ProductId { get; set; }
        public string ComponentName { get; set; }
        public string ComponentCode { get; set; }
        public int Quantity { get; set; }
        public string ColorName { get; set; }
        public string Size { get; set; }
    }
}
