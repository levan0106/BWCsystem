using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Model
{
    public class ProductMaterial:Base
    {
        public int ProductId { get; set; }
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
    }
}
