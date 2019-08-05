using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Authentication
{
    public enum TokenStatus
    {
        Passed =1,
        Failed = 2,
        Expired=3
    }
}
