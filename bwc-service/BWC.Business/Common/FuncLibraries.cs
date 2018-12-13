using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Business.Common
{
    public static class FuncLibraries
    {
        public static long DateTimeToTicks(this DateTime dateTime)
        {
            long ticks = 0;
            ticks = dateTime.Ticks;
            return ticks;
        }
        public static DateTime TicksToDateTime(this long ticks)
        {
            DateTime dateTime = new DateTime(ticks);
            return dateTime;
        }
    }
}
