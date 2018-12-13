using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Model
{
    public class TimeSheetLessPAYG: Base
    {
        public int EmployeeId { get; set; }
        public double LessPAYG { get; set; }
    }
}
