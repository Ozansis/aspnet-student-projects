using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace WebFormsHW1
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                calBirthDate.SelectedDate = DateTime.Today; 
            }
        }

        protected void calBirthDate_SelectionChanged(object sender, EventArgs e)
        {
            lblSelectedDate.Text = "Selected Date: " + calBirthDate.SelectedDate.ToString("dd.MM.yyyy");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string country = ddlCountry.SelectedValue;
            string gender = rblGender.SelectedValue;
            string birthDate = calBirthDate.SelectedDate.ToString("dd.MM.yyyy");

         
            List<string> hobbies = chkHobbies.Items.Cast<ListItem>()
                                  .Where(i => i.Selected)
                                  .Select(i => i.Text)
                                  .ToList();

            // Kullanıcının ülkesine göre en popüler 10 yer
            List<string> recommendedPlaces = GetRecommendedPlaces(country);

            
            string result = $"<strong>Name:</strong> {name}<br />" +
                            $"<strong>Country:</strong> {country}<br />" +
                            $"<strong>Gender:</strong> {gender}<br />" +
                            $"<strong>Birth Date:</strong> {birthDate}<br />" +
                            $"<strong>Hobbies:</strong> <ul>";

            foreach (string hobby in hobbies)
            {
                result += $"<li>{hobby}</li>";
            }
            result += "</ul>";

            // Önerilen yerleri ekle
            result += "<strong>Recommended Places:</strong> <ul>";
            foreach (string place in recommendedPlaces)
            {
                result += $"<li>{place}</li>";
            }
            result += "</ul>";

            lblResult.Text = result; 
        }

        // Kullanıcının seçtiği ülkeye göre önerilen 10 yer
        private List<string> GetRecommendedPlaces(string country)
        {
            Dictionary<string, List<string>> placeRecommendations = new Dictionary<string, List<string>>
            {
                { "USA", new List<string> { "Grand Canyon", "Yellowstone", "Statue of Liberty", "Times Square", "Disneyland", "Las Vegas Strip", "Niagara Falls", "Golden Gate Bridge", "Hollywood", "Miami Beach" } },
                { "UK", new List<string> { "Big Ben", "Buckingham Palace", "Stonehenge", "Tower of London", "British Museum", "Edinburgh Castle", "Lake District", "Windsor Castle", "Hyde Park", "Oxford University" } },
                { "Canada", new List<string> { "Niagara Falls", "Banff National Park", "CN Tower", "Old Quebec", "Stanley Park", "Whistler", "Capilano Suspension Bridge", "Peggy's Cove", "Jasper National Park", "Toronto Islands" } }
            };

            if (placeRecommendations.ContainsKey(country))
            {
                return placeRecommendations[country];
            }

            // Eğer ülke seçilmemişse 
            return new List<string> { "Eiffel Tower, France", "Colosseum, Italy", "Machu Picchu, Peru", "Great Wall of China", "Pyramids of Giza, Egypt", "Taj Mahal, India", "Mount Fuji, Japan", "Santorini, Greece", "Sydney Opera House, Australia", "Christ the Redeemer, Brazil" };
        }
    }
}
