using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DrawingOnline.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services;

namespace DrawingOnline.Controllers
{
    public class HomeController : Controller
    {
        public bool loggedIn = false;
        public int userID = 1;
        List<Drawing> userDrawings = new List<Drawing>();
        string drawingsJson;
        User user;
        List<UserDrawingClass> publicDrawings = new List<UserDrawingClass>();

        void loadUserdrawings()
        {
            using (DrawingOnlineEntities entitet = new DrawingOnlineEntities())
            {
                userDrawings = entitet.Drawings.Where(x => x.UserID == userID).ToList();
                List<UserDrawingClass> userDrawingsClass = userDrawings.Select(x => new UserDrawingClass
                {
                    ID = x.ID,
                    Name = x.Name,
                    Canvas = x.Canvas,
                    Remixed = x.Remixed,
                    RemixedID = x.RemixedID,
                    UserID = x.UserID,
                    Publicity = x.Publicity
                }).ToList();
                drawingsJson = Newtonsoft.Json.JsonConvert.SerializeObject(userDrawingsClass);
            }
        }
        public ActionResult Index()
        {
            
            return View();
        }

        
        public ActionResult Draw()
        {
            loadUserdrawings();
            ViewBag.Sider = drawingsJson;
            return View();
        }
        public ActionResult AddDrawing(UserDrawingClass drawing)
        {
            Drawing nr = new Drawing();
            nr.Name = drawing.Name;
            nr.Canvas = drawing.Canvas;
            nr.Remixed = drawing.Remixed;
            nr.RemixedID = drawing.RemixedID;
            nr.Publicity = drawing.Publicity;
            nr.UserID = userID;
            nr.CanvasSVG = drawing.CanvasSVG;

            using (DrawingOnlineEntities entitet = new DrawingOnlineEntities())
            {
                entitet.Drawings.Add(nr);
                entitet.SaveChanges();
                return Json(true);
            }
        }

        public ActionResult Profile()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult MyDrawings()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult PublicDrawings()
        {
            using(DrawingOnlineEntities entitet = new DrawingOnlineEntities())
            {
                publicDrawings = entitet.Drawings.Where(x => x.Publicity == true).ToList().Select(x => new UserDrawingClass
                {
                    ID = x.ID,
                    Name = x.Name,
                    Canvas = x.Canvas,
                    Remixed = x.Remixed,
                    RemixedID = x.RemixedID,
                    UserID = x.UserID,
                    Publicity = x.Publicity,
                    CanvasSVG = x.CanvasSVG
                }).ToList();
            }

            ViewBag.Message = "Your contact page.";

            return View(publicDrawings);
        }

    }
}