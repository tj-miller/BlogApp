using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.Controllers
{
    public class HomeController : Controller
    {
        BlogDataLayer bdl = new BlogDataLayer();
        public ActionResult Index()
        {
            List<BlogEntry> blogs = bdl.GetBlogs();
            ViewBag.Blogs = blogs;
            
            return View();
        }

        public ActionResult About()
        {
            
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}