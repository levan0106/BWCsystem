using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Model
{
    public class MakerSheet: Category
    {
        public int Index { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string MaterialName { get; set; }
        public double Width { get; set; }
        public double Drop { get; set; }
        public int Quantity { get; set; }
        public string ColorName { get; set; }
        public string HoodWidth { get; set; }
        public int BoxWidth { get; set; }
        public int BarWidth { get; set; }

        public string LocationName { get; set; }
        public string MaterialColorName { get; set; }
        public string BoxColorName { get; set; }
        public string BarColorName { get; set; }
        public string GuideColorName { get; set; }
        public string ControlColorName { get; set; }
        public string StripeColorName { get; set; }

        public string ControlSideId { get; set; }
        public string ControlSideName { get; set; }
        public string RollId { get; set; }
        public string RollName { get; set; }
        public string TSplineId { get; set; }
        public string TSplineName { get; set; }
        public string BSplineId { get; set; }
        public string BSplineName { get; set; }
        public string FlapId { get; set; }
        public string FlapName { get; set; }
        public string ControlHBOL { get; set; }
        public string TubeDia { get; set; }
        public string Notes { get; set; }
        public string BKTId { get; set; }
        public string BKTName { get; set; }
    }
}
