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
        public int userID = 2;
        List<Drawing> userDrawings = new List<Drawing>();
        
        public ActionResult Index()
        {
            IndexViewModel indexViewModel = new IndexViewModel();
            indexViewModel.userID = userID; 
            return View(indexViewModel);
        }

        
        public ActionResult Draw( string edit, string remix, string drawId, string remixId)
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
            }
            ViewBag.edit = edit; 
            ViewBag.drawId = drawId;
            ViewBag.remix = remix;
            ViewBag.remixId = remixId;

            Drawing remixoreditDrawing = new Drawing();

            remixoreditDrawing.Canvas = "";

            if (Convert.ToBoolean(remix))
            {    
                using (DrawingOnlineEntities entitet = new DrawingOnlineEntities())
                {
                    int re = Convert.ToInt32(remixId.Replace(";", "")); 
                    remixoreditDrawing = entitet.Drawings.Where(x => x.ID == re).FirstOrDefault();
                }
            }else if(Convert.ToBoolean(edit))
                using (DrawingOnlineEntities entitet = new DrawingOnlineEntities())
                {
                    int ed = Convert.ToInt32(drawId.Replace(";", ""));
                    remixoreditDrawing = entitet.Drawings.Where(x => x.ID == ed).FirstOrDefault();
                }
            return View(remixoreditDrawing);

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

        public ActionResult UpdateDrawing(UserDrawingClass drawing)
        {
            Drawing nr = new Drawing();
            nr.ID = drawing.ID;
            nr.Canvas = drawing.Canvas;
            nr.CanvasSVG = drawing.CanvasSVG;

            using (DrawingOnlineEntities entitet = new DrawingOnlineEntities())
            {
                entitet.Drawings.Where(x => x.ID == nr.ID).FirstOrDefault().Canvas = nr.Canvas;
                entitet.Drawings.Where(x => x.ID == nr.ID).FirstOrDefault().CanvasSVG = nr.CanvasSVG;
                entitet.SaveChanges();
            }
                return Json(true);
        }
        public ActionResult Profile()
        {
            ProfileViewModel profileViewModel = new ProfileViewModel();
            profileViewModel.user = new UserModelClass();
            if(userID != 1)
            {
                using (DrawingOnlineEntities entitet = new DrawingOnlineEntities())
                {
                    User user = entitet.Users.Where(x => x.ID == userID).FirstOrDefault();

                    profileViewModel.user = new UserModelClass
                    {
                        ID = user.ID,
                        Username = user.Username,
                        Password = user.Password,
                        Tagline = user.Tagline,
                        Name = user.Name
                    };
                    profileViewModel.userDrawings = entitet.Drawings.Where(x => x.UserID == user.ID).Select(x => new UserDrawingClass
                    {
                        ID = x.ID,
                        UserID = x.UserID,
                        Name = x.Name,
                        CanvasSVG = x.CanvasSVG,
                        Canvas = x.Canvas,
                        Publicity = x.Publicity,
                        Remixed = x.Remixed,
                        RemixedID = x.RemixedID
                    }).ToList();

                }    
            }
            ViewBag.Message = "Your application description page.";

            return View(profileViewModel);
        }
        public ActionResult addUser(UserModelClass newUser)
        {
            using (DrawingOnlineEntities entitet = new DrawingOnlineEntities())
            {
                if(newUser.Name != null && newUser.Username != null && newUser.Password != null)
                {
                    if(newUser.Password.ToCharArray().Length > 6)
                    {
                        bool unUsedUsername = true;
                        foreach (var exUser in entitet.Users)
                        {
                            if (exUser.Username == newUser.Username)
                            {
                                unUsedUsername = false;
                                break;
                            }
                        }
                        if (unUsedUsername)
                        {
                            User user = new User();
                            user.Name = newUser.Name;
                            user.Username = newUser.Username;
                            user.Password = newUser.Password;
                            user.Tagline = newUser.Tagline;

                            entitet.Users.Add(user);
                            entitet.SaveChanges();

                            return Json(true);
                        }
                        else
                        {
                            return Json("Username already in use");
                        }
                    }
                    else
                    {
                        return Json("Password to short");
                    }                   
                }     
            }
            return Json("Please check input fields");             
        }
        public ActionResult UserLogin(UserModelClass user)
        {
            User userMatch = new User();

            using (DrawingOnlineEntities entitet = new DrawingOnlineEntities())
            {
                userMatch = entitet.Users.Where(x => x.Username == user.Username).FirstOrDefault();

                if(userMatch != null)
                {
                    if(userMatch.Password == user.Password)
                    {
                        userID = userMatch.ID;
                        return Json(true);
                    }
                    else
                    {
                        return Json("Wrong Password");
                    }
                }
                else
                {
                    return Json("No matching username found");
                }
            }

            
        }
        public ActionResult MyDrawings()
        {
            MyDrawingsViewModel myDrawingsViewModel = new MyDrawingsViewModel();
            myDrawingsViewModel.user = new UserModelClass();
            if (userID != 1)
            {
                using (DrawingOnlineEntities entitet = new DrawingOnlineEntities())
                {
                    User user = entitet.Users.Where(x => x.ID == userID).FirstOrDefault();

                    myDrawingsViewModel.user = new UserModelClass
                    {
                        ID = user.ID,
                        Username = user.Username,
                        Password = user.Password,
                        Tagline = user.Tagline,
                        Name = user.Name
                    };
                    myDrawingsViewModel.userDrawings = entitet.Drawings.Where(x => x.UserID == user.ID).Select(x => new UserDrawingClass
                    {
                        ID = x.ID,
                        UserID = x.UserID,
                        Name = x.Name,
                        CanvasSVG = x.CanvasSVG,
                        Canvas = x.Canvas,
                        Publicity = x.Publicity,
                        Remixed = x.Remixed,
                        RemixedID = x.RemixedID
                    }).ToList();
                    myDrawingsViewModel.publicDrawings = entitet.Drawings.Where(x => x.Publicity == true).ToList().Select(x => new UserDrawingClass
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
            }

            return View(myDrawingsViewModel);
        }
        public ActionResult PublicDrawings()
        {
            PublicDrawingsViewModel publicDrawingsViewModel = new PublicDrawingsViewModel();

            using (DrawingOnlineEntities entitet = new DrawingOnlineEntities())
                {
                    publicDrawingsViewModel.publicDrawings = entitet.Drawings.Where(x => x.Publicity == true).ToList().Select(x => new UserDrawingClass
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

                publicDrawingsViewModel.users = entitet.Users.Select(x => new UserModelClass
                {
                    ID = x.ID,
                    Tagline = x.Tagline,
                    Username = x.Username,
                }).ToList();
                }
            ViewBag.Message = "Your contact page.";

            return View(publicDrawingsViewModel);
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Login()
        {

            return View();
        }
    }
}