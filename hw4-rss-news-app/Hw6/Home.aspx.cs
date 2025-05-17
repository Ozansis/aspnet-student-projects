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
    public partial class Home : System.Web.UI.Page
    {
        // Initialize NLog
        private static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAllNews();
            }
        }

        private void LoadAllNews()
        {
            ArrayList newsList = new ArrayList();
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string query = "SELECT ID, Title, Author, PubDate, ImageUrl, Category FROM News ORDER BY PubDate DESC";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        connection.Open();
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                try
                                {
                                    int newsID = reader["ID"] != DBNull.Value ? (int)reader["ID"] : 0;
                                    string title = reader["Title"] != DBNull.Value ? reader["Title"].ToString() : "";
                                    string author = reader["Author"] != DBNull.Value ? reader["Author"].ToString() : "Unknown";
                                    string pubDate = reader["PubDate"] != DBNull.Value ? reader["PubDate"].ToString() : "";
                                    string imageUrl = reader["ImageUrl"] != DBNull.Value ? reader["ImageUrl"].ToString() : "";
                                    string category = reader["Category"] != DBNull.Value ? reader["Category"].ToString() : "";

                                    // Create a News object and add it to the list
                                    News news = new News(newsID, title, "", category, author, pubDate, imageUrl);
                                    newsList.Add(news);
                                }
                                catch (IndexOutOfRangeException ex)
                                {
                                    // Log the exception 
                                    logger.Error(ex, "Column not found in database: {0}", ex.Message);
                                }
                            }
                        }
                    }
                }

                // Bind the news list to the repeater
                rptAllNews.DataSource = newsList;
                rptAllNews.DataBind();
            }
            catch (Exception ex)
            {
                // Log the exception
                logger.Error(ex, "Error accessing database: {0}", ex.Message);
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