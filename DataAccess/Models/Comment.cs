using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApp.Models
{
    public class Comment
    {
        private int id;
        private int associatedPost;
        private string content;
        private DateTime creationDate;
        public Comment()
        {

        }
    }
}