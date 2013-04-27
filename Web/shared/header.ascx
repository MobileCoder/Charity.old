<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="header.ascx.cs" Inherits="AwsWebApp1.shared.HeaderControl" %>
<script src="../js/header.js"></script>
<table width="100%">
    <tr>
        <td><h1>Charity</h1></td>
        <td width="50%" align="right">
            <div id="AnonymousLogin">
                Email: <input type="text" id="emailAddress" /><br />
                Password: <input type="password" id="password" /><br />
                <input type="button" id="LoginButton" value="LOGIN" />
            </div>
            <div id="LoggedIn">
                <div id="DisplayName"></div> (<input type="button" id="Logout" value="Logout" />)
            </div>
        </td>
    </tr>
</table>
<hr />