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


        public int ID { get { return id; } set { id = value; } }
        public int AssociatedPost { get { return associatedPost; } set { associatedPost = value; } }
        public string Content { get { return content; } set { content = value; } }
        public DateTime CreationDate { get { return creationDate; } set { creationDate = value; } }
    }
}