using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrawingOnline.Models
{
    public class PublicDrawingsViewModel
    {
        public List<UserDrawingClass> publicDrawings { get; set; }
        public List<UserModelClass> users { get; set; }
    }
}