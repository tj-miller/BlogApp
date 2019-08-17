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

            // close connection if open
            /*if (conn != null && conn.State == ConnectionState.Closed)
            {
                conn.Close();
            } */
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

        public BlogEntry GetBlogDetails(BlogEntry blog) {
            return null;
        }
    }
}