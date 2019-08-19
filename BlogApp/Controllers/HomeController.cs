using BlogApp.Models;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

            //reduce content for summary view
            foreach(BlogEntry entry in blogs)
            {
                entry.Content = GetBlogSummary(entry.Content);
            }
            ViewBag.Blogs = blogs;
            
            return View();
        }

        public ActionResult DetailView(int blogPostId)
        {

            ViewBag.Message = blogPostId.ToString();
            BlogEntry blog = bdl.GetBlogDetailByID(blogPostId);
            ViewBag.Blog = blog;

            return View();
        }

        public ActionResult Create()
        {

            return View();
        }


        public string GetBlogSummary(string content)
        {
            string output = "";

            string[] sentences = Regex.Split(content, @"(?<=[.?!])");
            for(int i = 0; i< sentences.Length && i<3; i++)
            {
                //get first three sentences
                    output += sentences[i];
            }
    
            return output;
        }

        [HttpPost]
        public void PostComment(int blogID, string comment)
        {

            bdl.PostComment(blogID, comment);
        }

    }
}