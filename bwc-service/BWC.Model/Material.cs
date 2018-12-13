using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Model
{
    public class Material:Base
    {
        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
    }
}
