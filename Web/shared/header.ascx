<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="header.ascx.cs" Inherits="AwsWebApp1.shared.HeaderControl" %>
<table width="100%">
    <tr>
        <td><h1>Charity</h1></td>
        <td width="50%" align="right">
            <div id="AnonymousLogin">
                <table>
                    <tr>
                        <td><div id="SignIn">Sign In</div></td>
                        <td> / </td>
                        <td><div id="Register">Register</div></td>
                    </tr>

                </table>
                
                <div id="SignInOptions">
                    Email: <input type="text" id="emailAddress" /><br />
                    Password: <input type="password" id="password" /><br />
                    <input type="button" id="ForgotPassword" value ="Forgot Password" />
                    <input type="button" id="LoginButton" value="Login" />
                </div>

                <div id="RegisterOptions">
                    Email: <input type="text" id="registerEmail" /><br />
                    <input type="button" id="RegisterButton" value="Register" />
                </div>

            </div>
            <div id="LoggedIn">
                <div id="DisplayName"></div> (<input type="button" id="Logout" value="Logout" />)
            </div>
        </td>
    </tr>
</table>
<hr />