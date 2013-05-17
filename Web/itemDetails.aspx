<%@ Page Title="" Language="C#" MasterPageFile="~/shared/Charity.Master" AutoEventWireup="true" CodeBehind="itemDetails.aspx.cs" Inherits="AwsWebApp1.itemDetails" %>
<%@ Register src="controls/ItemImageWebUserControl.ascx" tagname="ItemImageWebUserControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titlebar" runat="server"> 
    <asp:Literal ID="ItemTitle" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2><asp:Label ID="ItemDescription" runat="server"></asp:Label></h2>
    <uc1:ItemImageWebUserControl ID="images" runat="server" />
    <br />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="scripts" runat="server">
    <script type="text/javascript" src="js/itemDetails.js"></script>
</asp:Content>
