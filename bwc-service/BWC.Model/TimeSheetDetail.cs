using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Model
{
    public class TimeSheetDetail:Base
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int Day { get; set; }
        public string Month { get; set; }
        public double Value { get; set; }
        public int Type { get; set; }
        public bool Approved { get; set; }
    }
}
