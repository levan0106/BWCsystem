using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BWC.Common
{
    public class Enums
    {
        public enum OrderType
        {
            Purchase=1,
            Order=2,
            Quotation=3
        }
        public enum PriceType
        {
            Purchase = 1,
            Sell = 2
        }
    }
}