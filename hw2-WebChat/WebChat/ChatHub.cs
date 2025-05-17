using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace WebChat
{
    public class ChatHub : Hub
    {
        public override Task OnConnected()
        {
            var userId = Context.QueryString["userId"];
            if (!string.IsNullOrEmpty(userId))
                Groups.Add(Context.ConnectionId, userId);
            return base.OnConnected();
        }

        public async Task Send(int senderId, int receiverId, string message)
        {
            try
            {
                var connStr = ConfigurationManager.ConnectionStrings["ChatDB"].ConnectionString;
                // INSERT sorgusunda tablo ve sütunları köşeli parantezledik
                var sql = @"
                    INSERT INTO [CHAT]
                        ([SenderID], [ReceiverID], [MessageText], [Timestamp], [IsRead])
                    VALUES
                        (?, ?, ?, ?, ?)
                ";

                using (var conn = new OleDbConnection(connStr))
                using (var cmd = new OleDbCommand(sql, conn))
                {
                    // Parametreleri doğru OleDbType’larla ekliyoruz
                    cmd.Parameters.Add("pSender", OleDbType.Integer).Value = senderId;
                    cmd.Parameters.Add("pReceiver", OleDbType.Integer).Value = receiverId;
                    cmd.Parameters.Add("pMessage", OleDbType.VarChar, 255).Value = message;
                    cmd.Parameters.Add("pTimestamp", OleDbType.Date).Value = DateTime.Now;
                    cmd.Parameters.Add("pIsRead", OleDbType.Boolean).Value = false;

                    conn.Open();
                    await cmd.ExecuteNonQueryAsync();
                }

                // Başarılıysa alıcıya publish et
                await Clients.Group(receiverId.ToString())
                             .receiveMessage(senderId, message);
            }
            catch (Exception ex)
            {
                // Hata varsa çağıranda göster
                await Clients.Caller.receiveError(ex.ToString());
            }
        }
    }
}
