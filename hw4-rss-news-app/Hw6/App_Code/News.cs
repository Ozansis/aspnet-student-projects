using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hw6.App_Code
{
    public class News
    {
        public int NewsID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
        public string PubDate { get; set; }
        public string ImageUrl { get; set; }

        public News(int newsID, string title, string description, string category, string author, string pubDate, string imageUrl)
        {
            NewsID = newsID;
            Title = title;
            Description = description;
            Category = category;
            Author = author;
            PubDate = pubDate;
            ImageUrl = imageUrl;
        }
    }
}