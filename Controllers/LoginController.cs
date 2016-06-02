using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisitMarker.Models;

namespace VisitMarker.Controllers
{
    public class LoginController : Controller
    {

        //
        // GET: /Login/
        DataClasses1DataContext obj = new DataClasses1DataContext();

        public ActionResult Index()
        {
            Session["username"] = null;
            return View();
        }

        public ActionResult invalid()
        {
            return View();
        }

        public ActionResult general()
        {

            return View();
        }

        public ActionResult Validate()
        {


            //  UsersTable ut = new UsersTable();
            string email = Request["email"];
            string password = Request["password"];

             
                var a = from n in obj.Logins where n.email==email && n.pass==password select n; //obj.Logins.First(x => x.email == email && x.pass == password);


                var b = from n in obj.Admins where n.email == email && n.password == password select n; //obj.Admins.First(y => y.email == email && y.password == password);
                
                if (b.Any())
                {
                    string id=string.Empty;
                    foreach(var i in b)
                    {
                        id = i.Id.ToString();
                    }
                    Session["id"] = id;
                    Session["mail"] = email;
                    return RedirectToAction("index", "Admin");
                }

                //Session["invalid"] = "Invalid username or password. Try again.";
                //return RedirectToAction("index", "Login");

                else if (a.Any())
                {
                    string id=string.Empty;
                    string uname=string.Empty;
                    foreach (var i in a)
                    {
                        id = i.Id.ToString();
                        uname = i.uname.ToString();
                    }
                    Session["id"] = id;
                    Session["username"] = uname;
                    // Session["mail"] = email;
                    JavaScript("logout_visibility()");
                    return RedirectToAction("../Home/index");


                }
                else
                {
                    Session["invalid"] = "Invalid username or password. Try again.";
                    return RedirectToAction("index", "Login");
                }
                //   return View("~/Home/index");
        }

        public ActionResult AddUser()
        {

            string username = Request["usernamesignup"];
            string email = Request["emailsignup"];
            string pass = Request["passwordsignup"];
            string conpass = Request["passwordsignup_confirm"];


            if (pass == conpass)
            {
                Login log = new Login();

                log.uname = username;
                log.pass = pass;
                log.email = email;
                Session["username"] = username;
                obj.Logins.InsertOnSubmit(log);
                obj.SubmitChanges();
                return RedirectToAction("index", "Home");
            }
            else
            {
                Content("<script language='javascript' type='text/javascript'>alert('Your Password & Confirm Password does not match');</script>");
                return RedirectToAction("index", "Login");
            }


        }
    }
}
