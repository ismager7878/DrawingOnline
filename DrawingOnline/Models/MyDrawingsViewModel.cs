using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrawingOnline.Models
{
    public class MyDrawingsViewModel
    {
        public UserModelClass user { get; set; }
        public List<UserDrawingClass> userDrawings { get; set; }
        public List<UserDrawingClass> publicDrawings { get; set; }

    }
}