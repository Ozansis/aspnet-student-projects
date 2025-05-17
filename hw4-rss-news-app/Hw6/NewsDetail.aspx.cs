using System;
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
    public partial class NewsDetail : System.Web.UI.Page
    {
        // Initialize NLog
        private static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadNewsDetails();
            }
        }

        private void LoadNewsDetails()
        {
            int newsID;
            if (int.TryParse(Request.QueryString["NewsID"], out newsID))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
                string query = "SELECT Title, Description, Author, PubDate, ImageUrl, Category FROM News WHERE ID = ?";

                try
                {
                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {
                        using (OleDbCommand command = new OleDbCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ID", newsID);
                            connection.Open();
                            using (OleDbDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string title = reader["Title"].ToString();
                                    string description = reader["Description"].ToString();
                                    string author = reader["Author"].ToString();
                                    string pubDate = reader["PubDate"].ToString();
                                    string category = reader["Category"].ToString();
                                    string imageUrl = reader["ImageUrl"].ToString();

                                    // Set page title to news title
                                    Page.Title = title;

                                    // Fill in the literals
                                    ltTitle.Text = title;
                                    ltDescription.Text = description;
                                    ltAuthor.Text = author;

                                    // Format date
                                    try
                                    {
                                        DateTime date;
                                        if (DateTime.TryParse(pubDate, out date))
                                        {
                                            ltPubDate.Text = date.ToString("MMMM dd, yyyy");
                                        }
                                        else
                                        {
                                            ltPubDate.Text = pubDate;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        logger.Error(ex, "Error formatting date: {0}", ex.Message);
                                        ltPubDate.Text = pubDate;
                                    }

                                    ltCategory.Text = category;
                                    ltCategoryNav.Text = category;
                                    ltCategoryLink.Text = category;
                                    ltCategoryBadge.Text = category;

                                    // Set image URL or use placeholder if missing
                                    if (!string.IsNullOrEmpty(imageUrl))
                                    {
                                        imgNews.ImageUrl = imageUrl;
                                    }
                                    else
                                    {
                                        imgNews.ImageUrl = "https://via.placeholder.com/1200x600?text=No+Image+Available";
                                    }

                                    // For safety, add error handler
                                    imgNews.Attributes["onerror"] = "this.src='https://via.placeholder.com/1200x600?text=Image+Error'";
                                }
                                else
                                {
                                    // News item not found
                                    logger.Warn("News item with ID {0} not found", newsID);
                                    Response.Redirect("Home.aspx");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception
                    logger.Error(ex, "Error accessing database: {0}", ex.Message);
                    Response.Write("<div class='alert alert-danger'>Error loading news details. Please try again later.</div>");
                }
            }
            else
            {
                // Invalid or missing news ID
                logger.Warn("Invalid or missing news ID in request");
                Response.Redirect("Home.aspx");
            }
        }
    }
}