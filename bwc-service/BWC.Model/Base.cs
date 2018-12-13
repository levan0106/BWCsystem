using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Model
{
    public abstract class Base
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDTS { get; set; }
        public DateTime? UpdateDTS { get; set; }
        public Guid? CreateBy { get; set; }
        public Guid? UpdateBy { get; set; }
        public int? ActiveStatus { get; set; }
    }
}
