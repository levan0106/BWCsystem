using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Model
{
    public class Order:BaseOrder
    {       
        public int? SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierEmail { get; set; }
        public string SupplierPhone { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string OrderRefNo { get; set; }
        public double? InvoiceAmount { get; set; }
        public double? Balance { get; set; }        
        public double? AMTExcGST { get; set; }
        public double? GST { get; set; }
        public double? AMTIncGST { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string DeliveryNo { get; set; }
        public string InvoiceNoForOrderOnly { get; set; }
        public DateTime? InvoiceDateForOrderOnly { get; set; }
        public DateTime? PickupDateForOrderOnly { get; set; }
        public DateTime? CompleteDateForOrderOnly { get; set; }
        public string ProductIds { get; set; }

    }
}
