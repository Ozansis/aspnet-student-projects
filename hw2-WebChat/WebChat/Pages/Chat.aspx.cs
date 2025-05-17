using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Web.UI;

namespace WebChat.Pages
{
    public partial class Chat : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Oturum yoksa login’e gönder
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Pages/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                int currentUserId = Convert.ToInt32(Session["UserID"]);
                if (Request.QueryString["contactId"] == null)
                {
                    Response.Redirect($"~/Pages/Dashboard{Session["Role"]}.aspx");
                    return;
                }

                int contactId = Convert.ToInt32(Request.QueryString["contactId"]);
                hfReceiverId.Value = contactId.ToString();

                var dt = GetMessageHistory(currentUserId, contactId);
                rptMessages.DataSource = dt;
                rptMessages.DataBind();
            }
        }

        private DataTable GetMessageHistory(int user1, int user2)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ChatDB"].ConnectionString;

            // Tablo ve tüm sütun adlarını köşeli parantez [] içine aldık:
            string query = @"
        SELECT *
        FROM [CHAT]
        WHERE ([SenderID]   = ? AND [ReceiverID] = ?)
           OR ([SenderID]   = ? AND [ReceiverID] = ?)
        ORDER BY [Timestamp] ASC
    ";

            using (var conn = new OleDbConnection(connStr))
            using (var cmd = new OleDbCommand(query, conn))
            {
                // Parametrelerin tipi int-int-int-int eşleşecek:
                cmd.Parameters.AddWithValue("?", user1);
                cmd.Parameters.AddWithValue("?", user2);
                cmd.Parameters.AddWithValue("?", user2);
                cmd.Parameters.AddWithValue("?", user1);

                var adapter = new OleDbDataAdapter(cmd);
                var dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

    }
}
