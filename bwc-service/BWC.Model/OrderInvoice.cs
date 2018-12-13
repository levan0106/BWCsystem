using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Model
{
    public class OrderInvoice:Base
    {
        public object OrderId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public double InvoiceAmount { get; set; }
        public string CutLengthCharge { get; set; }
        public string DeliveryCharge { get; set; }
        public double? AMTExcGST { get; set; }
        public double? GST { get; set; }
        public double? AMTIncGST { get; set; }

    }
}
