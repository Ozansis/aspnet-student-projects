using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebChat
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["User_id"] != null)
            {
                LoadProfile();
            }
        }

        private void LoadProfile()
        {
            string connStr = ConfigurationManager.ConnectionStrings["ChatDB"].ConnectionString;
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                string query = "SELECT * FROM Users WHERE User_id = ?";
                OleDbCommand cmd = new OleDbCommand(query, conn);
                cmd.Parameters.AddWithValue("?", Session["User_id"]);

                conn.Open();
                OleDbDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtFullName.Text = reader["FullName"].ToString();
                    txtEmail.Text = reader["Email"].ToString();
                    txtPassword.Text = reader["Password"].ToString(); // for now, showing it
                    txtUsername.Text = reader["Username"].ToString();
                }

                conn.Close();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Session["User_id"] == null)
            {
                lblMessage.Text = "User session expired.";
                return;
            }

            int userId = Convert.ToInt32(Session["User_id"]);

            string connStr = ConfigurationManager.ConnectionStrings["ChatDB"].ConnectionString;
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                string query = $"UPDATE USERS SET FullName = ?, Email = ?, [Password] = ?, [Username] = ? WHERE User_id = {userId}";
                OleDbCommand cmd = new OleDbCommand(query, conn);
                cmd.Parameters.AddWithValue("?", txtFullName.Text.Trim());
                cmd.Parameters.AddWithValue("?", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("?", txtPassword.Text.Trim());
                cmd.Parameters.AddWithValue("?", txtUsername.Text.Trim());

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            lblMessage.Text = "Profile updated successfully!";
        }
    }
}