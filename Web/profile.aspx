<%@ Page Title="" Language="C#" MasterPageFile="~/shared/Charity.Master" AutoEventWireup="true" CodeBehind="profile.aspx.cs" Inherits="AwsWebApp1.profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titlebar" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="vertical-align: top">
        <tr>
            <td>Billing Address:</td>
            <td>
                <table>
                    <tr>
                        <td>Address:</td>
                        <td>
                            <input type="text" id="BillingAddress1" />
                            <input type="text" id="BillingAddress2" />
                        </td>
                    </tr>
                    <tr>
                        <td>City:</td>
                        <td><input type="text" id="BillingCity" /></td>
                    </tr>
                    <tr>
                        <td>State:</td>
                        <td><input type="text" id="BillingState" /></td>
                    </tr>
                    <tr>
                        <td>Zipcode:</td>
                        <td><input type="text" id="BillingZipcode" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <input type="button" id="updateBillingAddress" value="Update" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>Shipping Address:</td>
            <td>
                <table>
                    <tr>
                        <td>Address:</td>
                        <td>
                            <input type="text" id="ShippingAddress1" />
                            <input type="text" id="ShippingAddress2" />
                        </td>
                    </tr>
                    <tr>
                        <td>City:</td>
                        <td><input type="text" id="ShippingCity" /></td>
                    </tr>
                    <tr>
                        <td>State:</td>
                        <td><input type="text" id="ShippingState" /></td>
                    </tr>
                    <tr>
                        <td>Zipcode:</td>
                        <td><input type="text" id="ShippingZipcode" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <input type="button" id="updateShippingAddress" value="Update" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="scripts" runat="server">
    <script type="text/javascript" src="js/profile.js"></script>
</asp:Content>
