<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemWebUserControl.ascx.cs" Inherits="AwsWebApp1.ItemWebUserControl" %>
<table border="1">
    <tr>
        <td><asp:Label ID="TitleControl" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td><asp:Label ID="DescriptionControl" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td>Current Bid:<span ID="CurrentBidSpan" runat="server" /></td>
    </tr>
    <tr>
        <td>
            <div id="DivBidding" runat="server"></div>
        </td>
    </tr>
</table>