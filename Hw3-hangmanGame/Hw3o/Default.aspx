<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Hw3o.Default" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HangWoman Oyunu</title>
    
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://code.jquery.com/ui/1.12.1/themes/smoothness/jquery-ui.css" rel="stylesheet" />

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>

    <style>
        
        body {
            background-color: #f0f0f0;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            margin: 0;
            padding: 0;
        }
        .header {
            background-color: #FFD34E;
            padding: 20px;
            text-align: center;
        }
        .header h1 {
            font-size: 2.5rem;
            margin: 0;
        }
        .header h1 span {
            color: red;
        }
        
        .balloon-man {
            position: fixed;
            top: 20px;               
            left: 50%;
            transform: translateX(-50%);
            width: 250px;
            height: 300px;
            object-fit: contain;
            transition: top 0.5s ease; 
            z-index: 999;
        }
        
        .guess-area {
            position: fixed;
            bottom: 0;
            left: 50%;
            transform: translateX(-50%);
            width: 90%;
            max-width: 600px;
            background-color: #fff;
            border: 2px solid #FFD34E;
            border-radius: 10px;
            padding: 20px;
            margin-bottom: 20px;
            box-shadow: 0 2px 5px rgba(0,0,0,0.2);
        }
        .timer {
            font-size: 1.5rem;
            color: red;
            text-align: center;
            margin-bottom: 20px;
        }
        .question-label {
            font-size: 1.3rem;
            text-align: center;
            margin-bottom: 20px;
        }
        .word-display {
            font-size: 2rem;
            letter-spacing: 0.3rem;
            text-align: center;
            margin: 20px 0;
        }
        .letters-container {
            display: flex;
            flex-wrap: wrap;
            justify-content: center;
        }
        .letter-button {
            width: 45px;
            height: 45px;
            border-radius: 50%;
            margin: 5px;
            font-weight: bold;
            background-color: #FFD34E;
            border: 2px solid #ffac00;
            color: #333;
            transition: background-color 0.3s;
        }
        .letter-button:hover {
            background-color: #ffac00;
            color: #fff;
        }
        .letter-button.disabled {
            background-color: #cccccc;
            border-color: #999999;
            color: #666666;
            cursor: not-allowed;
        }
        .message {
            text-align: center;
            font-size: 1.3rem;
            margin-top: 15px;
        }
        .start-btn {
            display: block;
            margin: 0 auto 20px;
            background-color: #28a745;
            color: #fff;
        }
    </style>

    <script type="text/javascript">
        var timeLeft = 30;
        var timerInterval;
        function startTimer() {
            timeLeft = 30;
            timerInterval = setInterval(function () {
                timeLeft--;
                $('#timerLabel').text('Kalan Süre: ' + timeLeft + ' sn');
                if (timeLeft <= 0) {
                    clearInterval(timerInterval);
                    alert('Süre doldu! Kaybettiniz.');
                    $('.letter-button').prop('disabled', true);
                }
            }, 1000);
        }
        function stopTimer() {
            clearInterval(timerInterval);
        }
        // Yanlış tahminde balon patlama sesi
        function playBalloonPopSound() {
            var audio = new Audio('audio/balloonPop.mp3');
            audio.play();
        }
        // Oyun bitiş sesi
        function playGameOverSound(result) {
            var audio;
            if (result === 'win') {
                audio = new Audio('audio/winSound.mp3');
            } else if (result === 'lose') {
                audio = new Audio('audio/loseSound.mp3');
            }
            audio.play();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        
        <div class="header">
            <h1>Play <span>HangMan</span></h1>
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                
                <asp:Image ID="imgBalloonMan" runat="server" 
                           ImageUrl="~/Images/balloonManFirst.png" 
                           CssClass="balloon-man" ClientIDMode="Static" />

                <div class="guess-area">
                    <div id="timerLabel" class="timer">Kalan Süre: 30 sn</div>
                    <asp:Label ID="lblQuestion" runat="server" CssClass="question-label"></asp:Label>
                    <asp:Label ID="lblWord" runat="server" CssClass="word-display"></asp:Label>
                    <div class="letters-container">
                        <asp:Repeater ID="rptLetters" runat="server" OnItemCommand="rptLetters_ItemCommand">
                            <ItemTemplate>
                                <asp:Button ID="btnLetter" runat="server" 
                                            Text='<%# Eval("Letter") %>' 
                                            CommandName="Guess" 
                                            CommandArgument='<%# Eval("Letter") %>' 
                                            CssClass="letter-button" />
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <asp:Label ID="lblMessage" runat="server" CssClass="message text-danger"></asp:Label>
                    <asp:Button ID="btnStart" runat="server" CssClass="btn start-btn" 
                                Text="Oyunu Başlat" OnClick="btnStart_Click" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
