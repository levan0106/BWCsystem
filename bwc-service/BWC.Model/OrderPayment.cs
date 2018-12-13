using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Model
{
    public class OrderPayment:Base
    {
        public object OrderId { get; set; }
        public DateTime? DatePaid { get; set; }
        public string PaymentType { get; set; }
        public double AmountPaid { get; set; }

    }
}
