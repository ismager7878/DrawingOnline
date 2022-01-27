using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrawingOnline.Models
{
    public class DrawViewModel
    {
        public int userID { get; set; }
        public Drawing drawing  { get; set; }
    }
}