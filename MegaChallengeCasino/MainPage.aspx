<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="MegaChallengeCasino.MainPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Image ID="imageReelOne" runat="server" Height="175px" Width="175px" />
            <asp:Image ID="imageReelTwo" runat="server" Height="175px" Width="175px" />
            <asp:Image ID="imageReelThree" runat="server" Height="175px" Width="175px" />
            <br />
            <br />
            Your Bet:&nbsp;
            <asp:TextBox ID="textBoxPlaceBet" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="buttonPullLever" runat="server" Text="Pull The Lever!" OnClick="buttonPullLever_Click" />
            <br />
            <br />
            <asp:Label ID="labelResult" runat="server"></asp:Label>
            <br />
            <h3>
                Player Money:&nbsp;
            <asp:Label ID="labelMoney" runat="server"></asp:Label>
            </h3>
            <hr />
            <h3>1 Cherry = 2x Your Bet<br />
            2 Cherries = 3x Your Bet<br />
            3 Cheeries = 4x Your Bet<br />
            3 7&#39;s (Jackpot) = 100x Your Bet<br />
            HOWEVER....... If there is even one BAR you win nothing!</h3>
        </div>
    </form>
</body>
</html>
