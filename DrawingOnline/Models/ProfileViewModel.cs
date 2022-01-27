using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrawingOnline.Models
{
    public class ProfileViewModel
    {
        public UserModelClass user { get; set; }
        public List<UserDrawingClass> userDrawings { get; set; }
    }
}