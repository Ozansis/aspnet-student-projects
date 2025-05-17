using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Net;
using System.Xml;
using Hw6.App_Code;
using System.Data.OleDb;
using System.Configuration;

namespace Hw6
{
    public partial class RSSparsing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnParse_Click(object sender, EventArgs e)
        {
            ArrayList newsList = new ArrayList();
            int newsID = 1;

            // URL of the RSS feed
            string[] rssUrls = {
                "https://www.haberturk.com/rss/kategori/dunya.xml", // World
                "https://www.haberturk.com/rss/kategori/teknoloji.xml", // Technology
                "https://www.haberturk.com/rss/spor.xml" // Sports
            };

            foreach (string rssUrl in rssUrls)
            {
                XmlDocument rssXmlDoc = new XmlDocument();
                rssXmlDoc.Load(rssUrl);

                XmlNodeList rssNodes = rssXmlDoc.SelectNodes("rss/channel/item");

                foreach (XmlNode rssNode in rssNodes)
                {
                    if (newsList.Count >= 60) break; // Stop after collecting 60 news items

                    string title = rssNode["title"].InnerText;
                    string description = rssNode["description"].InnerText;
                    string category = DetermineCategory(rssUrl);
                    string author = rssNode["author"] != null ? rssNode["author"].InnerText : "Unknown";
                    string pubDate = rssNode["pubDate"].InnerText;
                    string imageUrl = rssNode["media:content"] != null ? rssNode["media:content"].Attributes["url"].Value : "";

                    // Check if the news item already exists in the database
                    bool newsExists = false;
                    string connectionString = ConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
                    string query = "SELECT COUNT(*) FROM News WHERE Title = ?";

                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {
                        using (OleDbCommand command = new OleDbCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@title", title);

                            connection.Open();
                            int count = (int)command.ExecuteScalar();
                            if (count > 0)
                            {
                                // News item already exists in the database
                                newsExists = true;
                            }
                        }
                    }

                    if (!newsExists)
                    {
                        // Insert the news item into the Access database
                        InsertIntoAccessDatabase(new News(newsID++, title, description, category, author, pubDate, imageUrl));

                        // Add the news item to the ArrayList
                        News news = new News(newsID - 1, title, description, category, author, pubDate, imageUrl);
                        newsList.Add(news);
                    }
                }
            }

            // Store the newsList in Application state for later use
            Application["NewsList"] = newsList;

            // Display result (for debugging purposes)
            ltlResult.Text = $"Parsed {newsList.Count} news items.";
        }

        private string DetermineCategory(string rssUrl)
        {
            // Determine the category based on the RSS URL
            if (rssUrl.Contains("dunya")) return "World";
            if (rssUrl.Contains("teknoloji")) return "Technology";
            if (rssUrl.Contains("spor")) return "Sports";

            return "Other";
        }

        private void InsertIntoAccessDatabase(News newsItem)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;

            string query = "INSERT INTO News (Title, Description, Category, Author, PubDate, ImageUrl) VALUES (?, ?, ?, ?, ?, ?)";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@title", newsItem.Title);
                    command.Parameters.AddWithValue("@description", newsItem.Description);
                    command.Parameters.AddWithValue("@category", newsItem.Category);
                    command.Parameters.AddWithValue("@author", newsItem.Author);
                    command.Parameters.AddWithValue("@pubDate", newsItem.PubDate);
                    command.Parameters.AddWithValue("@imageUrl", newsItem.ImageUrl);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}