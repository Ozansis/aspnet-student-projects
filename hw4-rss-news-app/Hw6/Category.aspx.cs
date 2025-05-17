using Hw6.App_Code;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NLog;
using System.Configuration;

namespace Hw6
{
    public partial class Category : System.Web.UI.Page
    {
        // Initialize NLog
        private static Logger logger = LogManager.GetCurrentClassLogger();
        protected string CategoryName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategoryNews();
            }
        }

        private void LoadCategoryNews()
        {
            // Get the category from the query string
            string category = Request.QueryString["Category"];

            if (!string.IsNullOrEmpty(category))
            {
                CategoryName = category;

                ArrayList newsList = new ArrayList();
                string connectionString = ConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
                string query = "SELECT ID, Title, Author, PubDate, ImageUrl FROM News WHERE Category = ? ORDER BY PubDate DESC";

                try
                {
                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {
                        using (OleDbCommand command = new OleDbCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Category", category);
                            connection.Open();
                            using (OleDbDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int newsID = reader["ID"] != DBNull.Value ? (int)reader["ID"] : 0;
                                    string title = reader["Title"] != DBNull.Value ? reader["Title"].ToString() : "";
                                    string author = reader["Author"] != DBNull.Value ? reader["Author"].ToString() : "Unknown";
                                    string pubDate = reader["PubDate"] != DBNull.Value ? reader["PubDate"].ToString() : "";
                                    string imageUrl = reader["ImageUrl"] != DBNull.Value ? reader["ImageUrl"].ToString() : "";

                                    News news = new News(newsID, title, "", category, author, pubDate, imageUrl);
                                    newsList.Add(news);
                                }
                            }
                        }
                    }

                    rptCategoryNews.DataSource = newsList;
                    rptCategoryNews.DataBind();
                }
                catch (Exception ex)
                {
                    // Log the exception
                    logger.Error(ex, "Error accessing database: {0}", ex.Message);
                    // Display error message to user
                    Response.Write("<div class='alert alert-danger'>Error loading news. Please try again later.</div>");
                }
            }
            else
            {
                // Redirect to home page if category is not provided
                logger.Warn("Category parameter missing in request");
                Response.Redirect("Home.aspx");
            }
        }

        // Helper method to format the publication date
        protected string FormatDate(string dateString)
        {
            try
            {
                DateTime date;
                if (DateTime.TryParse(dateString, out date))
                {
                    return date.ToString("MMM dd, yyyy");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error formatting date: {0}", ex.Message);
            }

            return dateString;
        }
    }
}