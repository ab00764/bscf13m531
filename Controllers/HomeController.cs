using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using VisitMarker.Models;

namespace VisitMarker.Controllers
{
    public class HomeController : Controller
    {
        DataClasses1DataContext obj = new DataClasses1DataContext();
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Article()
        {
            return View();
        }

        public ActionResult ContactUS()
        {
            return View();
        }

        public ActionResult temp()
        {
            return View();
        }

        public void Upload()
        {
            Content("<script language='javascript' type='text/javascript'>alert('Your Password & Confirm Password does not match');</script>");
        }
        
        
        [HttpPost]
        public ActionResult SaveArticle()
        {
            if (Session["username"] != null)
            {
                AllArticle article = new AllArticle();


                string name = Request["name"];
                string loc = Request["location"];
                string des = Request["message"];
                int id=Convert.ToInt32(Session["id"]);
                article.Name = name;
                article.Location = loc;
                article.Description = des;
                article.Id1 = id;
                // article.Id1 = 1;

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];
                    string img = @"~\Files\" + file.FileName;
                    article.image = @"/Files/" + file.FileName;
                    obj.AllArticles.InsertOnSubmit(article);
                    obj.SubmitChanges();
                    file.SaveAs(Server.MapPath(img));

                }
                return RedirectToAction("index", "Home");
            }
            else
            {
                return RedirectToAction("index", "Login");
            }
        }
        [HttpPost]
        public ActionResult Save()
        {
            Map map = new Map();
            string lattitude = Request["lat"];
            string longitude = Request["lng"];

            map.Lat = Convert.ToDouble(lattitude);
            map.Long = Convert.ToDouble(longitude);


            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                string img = @"~\Files\" + file.FileName;
                map.image = @"/Files/" + file.FileName;
                obj.Maps.InsertOnSubmit(map);
                obj.SubmitChanges();
                file.SaveAs(Server.MapPath(img));

            }


            return RedirectToAction("index", "Home");
        }

        public ActionResult ArticleDetails()
        {
            return View(obj.AllArticles.ToList());
        }
        public ActionResult index()
        {
            return View(obj.Maps.ToList());
        }
        //public JsonResult funct()
        //{

        //    return Json(true, JsonRequestBehavior.AllowGet);
        //}

    }
}
