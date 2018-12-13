using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Model
{
    public class Category: Base
    {
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public string Description { get; set; }
        // Common
        public int TubeWidth { get; set; }// Roller Blind
        public int TubeTypewidthLess { get; set; }
        public int TubeTypeWidthGreater { get; set; }
        public int BottomRailWidth { get; set; }// Roller Blind
        public int MaterialWidth { get; set; }// Roller Blind
        public int MaterialDrop { get; set; }// Roller Blind
        public bool HoodEqualWidth { get; set; }

        // Security, FlyDoor
        public int FrameWidth { get; set; }// FlyScreen
        public int FrameDrop { get; set; }// FlyScreen
        public int MeshWidth { get; set; }
        public int MeshDrop { get; set; }

        // ZipScreen
        //public int TubeWidth { get; set; }
        public int TubeType { get; set; }
        //public int BottomRailWidth { get; set; }
        //public int MaterialWidth { get; set; }
        //public int MaterialDrop { get; set; }
        public int OuterTrackDrop { get; set; }
        public int InnerTrackDrop { get; set; }
        public int MaterialKederWidth { get; set; }

        // RS
        public int BoxTypeDrop { get; set; }
        public int BoxLength { get; set; }
        public int BottomBarHeight { get; set; }
        public int BottomBarLength { get; set; }
        public int SlatHeight { get; set; }
        public int SlatAmount { get; set; }
        public int SlatLenght { get; set; }
        public int AxleLenght { get; set; }
        public int GuideLenght { get; set; }
    }
}
