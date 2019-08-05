using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Model
{
    public class OrderProduct : Base
    {
        public Int64? OrderId { get; set; }
        public int? OrderType { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int? LocationId { get; set; }
        public string LocationName { get; set; }
        public int? ColorId { get; set; }
        public string ColorName { get; set; }
        public int? UnitId { get; set; }
        public string UnitName { get; set; }
        public int? Drop { get; set; }
        public int? Width { get; set; }
        public int? Quantity { get; set; }
        public double? Discount { get; set; }
        public double? ExtendPrice { get; set; }
        public double? UnitPrice { get; set; }
        public double? TotalAmount { get; set; }
        public string DeliveryNo { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int? Received { get; set; }
        public int? BackOrder { get; set; }
        public int? Step { get; set; }
        public double? AMTExcGST { get; set; }
        public double? GST { get; set; }
        public double? AMTIncGST { get; set; }
        public double? ReceivedAMTExcGST { get; set; }
        public double? ReceivedGST { get; set; }
        public double? ReceivedAMTIncGST { get; set; }
        public string OrderRefNo { get; set; }

        public int? MaterialId { get; set; }
        public string MaterialName { get; set; }
        public string MaterialColorName { get; set; }
        public string BoxColorName { get; set; }
        public string BarColorName { get; set; }
        public string GuideColorName { get; set; }
        public string ControlColorName { get; set; }

        public int? ControlSideId { get; set; }
        public string ControlSideName { get; set; }
        public int? RollId { get; set; }
        public string RollName { get; set; }
        public int? TSplineId { get; set; }
        public string TSplineName { get; set; }
        public int? BSplineId { get; set; }
        public string BSplineName { get; set; }
        public int? FlapId { get; set; }
        public string FlapName { get; set; }
        public string ControlHBOL { get; set; }
        public string TubeDia { get; set; }
        public string Notes{ get; set; }
    }
}
