using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrawingOnline.Models
{
    public class UserDrawingClass
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Canvas { get; set; }
        public bool Remixed { get; set; }
        public Nullable<int> RemixedID { get; set; }
        public Nullable<int> UserID { get; set; }
        public bool Publicity { get; set; }

        public string CanvasSVG { get; set; }
    }
}