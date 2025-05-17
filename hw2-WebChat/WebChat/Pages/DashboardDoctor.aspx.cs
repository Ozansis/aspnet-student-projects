using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebChat.Pages
{
    public partial class DashboardDoctor : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblWelcome.Text = "Hello, Dr. " + Session["FullName"];

                rptContacts.DataSource = GetContacts();
                rptContacts.DataBind();
            }
        }
        private DataTable GetContacts()
        {
            string connStr = ConfigurationManager.ConnectionStrings["ChatDB"].ConnectionString;
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                string query = "SELECT User_id, FullName, Username FROM USERS WHERE Role = ?";
                OleDbCommand cmd = new OleDbCommand(query, conn);
                cmd.Parameters.AddWithValue("?", "Patient");

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
        
    }
}