<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="WebChat.Pages.Chat" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chat</title>
    <style>
        /* Buraya mevcut stil kodlarını alabilirsiniz */
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="chat-container">
            <asp:HiddenField ID="hfReceiverId" runat="server" />
            <div class="message-container" id="messageArea">
                <asp:Repeater ID="rptMessages" runat="server">
                    <ItemTemplate>
                        <div class='<%# Eval("SenderID").ToString() == Session["UserID"].ToString() ? "my-message" : "their-message" %>'>
                            <%# Eval("MessageText") %><br />
                            <small><%# Eval("Timestamp", "{0:HH:mm:ss}") %></small>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="input-row">
                <asp:TextBox ID="txtMessage" runat="server" Width="100%" />
                <asp:Button ID="btnSend" runat="server"
                            Text="Send"
                            OnClientClick="sendMessage(); return false;" />
            </div>
            <div id="typingIndicator"></div>
        </div>
    </form>

    <!-- Script dosyaları mutlaka kök Scripts klasöründen -->
    <script src="/Scripts/jquery-3.7.0.min.js"></script>
    <script src="/Scripts/jquery.signalR-2.4.3.min.js"></script>
    <script src="/signalr/hubs"></script>

    <script type="text/javascript">
        // Session["UserID"] mutlaka int olarak set edilmiş olmalı
        var currentUserId = <%= Session["UserID"] %>;

        $(function () {
            var chat = $.connection.chatHub;

            chat.client.receiveMessage = function (senderId, message) {
                var css = (senderId === currentUserId ? 'my-message' : 'their-message');
                $('#messageArea').append('<div class="' + css + '">' + message + '</div>');
            };
            chat.client.receiveError = function (errText) {
                alert("Server error:\n" + errText);
            };

            $.connection.hub.qs = { userId: currentUserId };
            $.connection.hub.start()
                .done(function () { console.log("SignalR connected."); })
                .fail(function (err) {
                    alert("SignalR connection failed:\n" + err.toString());
                });

            // Enter tuşu ile de mesaj gönder
            $('#<%= txtMessage.ClientID %>').keypress(function (e) {
                if (e.which === 13) {
                    e.preventDefault();
                    sendMessage();
                }
            });
        });

        function sendMessage() {
            var text = $('#<%= txtMessage.ClientID %>').val().trim();
            var recv = parseInt($('#<%= hfReceiverId.ClientID %>').val(), 10);
            if (!text) return;

            $.connection.chatHub.server.send(currentUserId, recv, text)
                .done(function () {
                    $('#messageArea').append('<div class="my-message">' + text + '</div>');
                    $('#<%= txtMessage.ClientID %>').val('');
                })
                .fail(function (err) {
                    var detail = err.responseText || JSON.stringify(err);
                    alert("Invoke error:\n" + detail);
                });
        }
    </script>
</body>
</html>
