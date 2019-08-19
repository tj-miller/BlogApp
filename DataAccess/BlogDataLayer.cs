using BlogApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess
{
    public class BlogDataLayer
    {

        private static SqlConnection connection = new SqlConnection();
        private static SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

        public static void Main(string[] args)
        {
                       
        }

        public void InitData()
        {
                builder = new SqlConnectionStringBuilder
                {
                    DataSource = "blogdbserv.database.windows.net",
                    UserID = "tmiller",
                    Password = "tjm2254A",
                    InitialCatalog = "BlogDB"
                };

                connection = new SqlConnection(builder.ConnectionString);

        }

        public List<BlogEntry> GetBlogs()
        {
            InitData();
            List<BlogEntry> blogs = new List<BlogEntry>();

            using (connection)
            {
                connection.Open();

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT ID, Title, Content, CreationDate ");
                sb.Append("FROM [BlogPost] ");
                sb.Append("ORDER BY CreationDate DESC");

                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BlogEntry blog = new BlogEntry();
                            blog.ID = (int) reader.GetValue(0);
                            blog.Title = reader.GetValue(1).ToString();
                            blog.Content = reader.GetValue(2).ToString();
                            blog.CreationDate = (DateTime)reader.GetValue(3);

                            blogs.Add(blog);
                        }
                    }
                }
            }        
            
            return blogs;
        }


        public BlogEntry GetBlogDetailByID(int blogPostId) {
            InitData();
            BlogEntry blog = new BlogEntry();

            using (connection)
            {
                connection.Open();

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT * ");
                sb.Append("FROM [BlogPost]");
                sb.Append("Where ID = " + blogPostId + " ");

                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            blog.ID = (int)reader.GetValue(0);
                            blog.Title = reader.GetValue(1).ToString();
                            blog.Content = reader.GetValue(2).ToString();
                            blog.CreationDate = (DateTime)reader.GetValue(3);
                           
                        }
                    }
                }
            }

            blog = AttachBlogComments(blog);
            return blog;


        }

        public BlogEntry AttachBlogComments(BlogEntry blog)
        {
            InitData();
            List<Comment> comments = new List<Comment>();
            using (connection)
            {
                connection.Open();

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT * ");
                sb.Append("FROM [Comment] ");
                sb.Append("Where PostID = " + blog.ID + " ");
                sb.Append("Order by CreationDate Desc");

                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Comment c = new Comment();
                            c.ID = (int)reader.GetValue(0);
                            c.AssociatedPost = (int)reader.GetValue(1);
                            c.Content = reader.GetValue(2).ToString();
                            c.CreationDate = (DateTime)reader.GetValue(3);

                            comments.Add(c);
                        }
                    }
                }
                blog.Comments = comments;

                return blog;
            }
        }

        public void PostComment(int blogId, string comment)
        {
            if (blogId < 1)
                return;

            InitData();
            using (connection)
            {
                connection.Open();

                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT into ");
                sb.Append("[Comment] (PostID, Content, CreationDate)");
                sb.Append("Values (" + blogId + ", " + comment + ", CURRENT_TIMESTAMP");

                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    int affectedRows = command.ExecuteNonQuery();

                }

            }

        }
    }
}