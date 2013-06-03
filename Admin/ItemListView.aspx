<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ItemListView.aspx.cs" Inherits="Admin.ItemListView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr style="vertical-align: top">
            <td>
                <asp:DropDownList ID="StatusList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="StatusList_SelectedIndexChanged"></asp:DropDownList>
                <br />
                <asp:ListBox ID="ItemList" runat="server" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="ItemList_SelectedIndexChanged"></asp:ListBox>
            </td>
            <td>
                <table>
                    <tr>
                        <td>Description:</td>
                        <td><asp:TextBox ID="Description" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Status:</td>
                        <td><asp:DropDownList ID="ItemStatus" runat="server" OnSelectedIndexChanged="ItemStatus_SelectedIndexChanged"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>Start Date:</td>
                        <td>
                        </td>
                    </tr>
                    <tr id="PurchasedByRow" runat="server">
                        <td>Purchased By:</td>
                        <td><asp:Label ID="PurchasedBy" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
