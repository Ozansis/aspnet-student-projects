using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebChat.Pages
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string role = rblRole.SelectedValue;

            string connStr = ConfigurationManager.ConnectionStrings["ChatDB"].ConnectionString;
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                try
                {
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = ?";
                    OleDbCommand checkCmd = new OleDbCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("?", username);

                    conn.Open();
                    int userExists = (int)checkCmd.ExecuteScalar();

                    if (userExists > 0)
                    {
                        lblMessage.CssClass = "error";
                        lblMessage.Text = "Username already exists.";
                        return;
                    }

                    string query = "INSERT INTO Users (FullName, Email, [Password], [Username], [Role]) VALUES (?, ?, ?, ?, ?)";
                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    cmd.Parameters.AddWithValue("?", fullName);
                    cmd.Parameters.AddWithValue("?", email);
                    cmd.Parameters.AddWithValue("?", password);
                    cmd.Parameters.AddWithValue("?", username);
                    cmd.Parameters.AddWithValue("?", role);

                    cmd.ExecuteNonQuery();

                    lblMessage.CssClass = "message";
                    lblMessage.Text = "Registration successful!";
                }
                catch (Exception ex)
                {
                    lblMessage.CssClass = "error";
                    lblMessage.Text = "Error: " + ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}