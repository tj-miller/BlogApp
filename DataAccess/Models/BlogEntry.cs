using BlogApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogApp.Models
{
    public class BlogEntry {
        private int id;
        private string title;
        private string content;
        private DateTime creationDate;
        private List<Comment> comments = new List<Comment>();

        public BlogEntry()
        {

        }

        public int ID { get { return id; } set { id = value; } }
        public string Title { get { return title; } set { title = value; } }
        public string Content { get { return content; } set { content = value; } }
        public DateTime CreationDate { get { return creationDate; } set { creationDate = value; } }
        public List<Comment> Comments { get { return comments; } set { comments = value; } }
    }
}
