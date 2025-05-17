using System;
using System.Configuration;
using System.Data.OleDb;
using System.Web.UI;

namespace WebChat.Pages
{
    public partial class Login : Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            string connStr = ConfigurationManager.ConnectionStrings["ChatDB"].ConnectionString;
            using (var conn = new OleDbConnection(connStr))
            using (var cmd = new OleDbCommand(
                "SELECT User_id, Username, Role, FullName FROM USERS WHERE Username = ? AND Password = ?",
                conn))
            {
                cmd.Parameters.AddWithValue("?", username);
                cmd.Parameters.AddWithValue("?", password);
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    // Chat.aspx'in beklediği anahtar:
                    Session["UserID"] = Convert.ToInt32(reader["User_id"]);
                    Session["Username"] = reader["Username"].ToString();
                    Session["Role"] = reader["Role"].ToString();
                    Session["FullName"] = reader["FullName"].ToString();

                    // Dashboard'a yönlendir
                    string role = reader["Role"].ToString();
                    if (role == "Doctor")
                        Response.Redirect("~/Pages/DashboardDoctor.aspx");
                    else
                        Response.Redirect("~/Pages/DashboardPatient.aspx");
                }
                else
                {
                    lblMessage.Text = "Geçersiz kullanıcı adı veya parola.";
                }
            }
        }
    }
}
