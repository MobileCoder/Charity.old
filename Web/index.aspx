<%@ Page Title="" Language="C#" MasterPageFile="~/shared/Charity.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="AwsWebApp1.index" %>
<%@ Reference Control="~/controls/ItemWebUserControl.ascx" %>    
<asp:Content ID="Content3" ContentPlaceHolderID="titlebar" runat="server">
    Charity
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="scripts" runat="server">
    <script type="text/javascript" src="js/index.js"></script>
    <script type="text/javascript" src="js/item.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input id="CreateItem" type="button" value="Create Item"/>
    <div id="ItemsDiv" runat="server"></div>
</asp:Content>
