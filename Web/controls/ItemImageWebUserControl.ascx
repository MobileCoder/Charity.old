<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemImageWebUserControl.ascx.cs" Inherits="AwsWebApp1.controls.ItemImageWebUserControl" %>
<div id="largeImage" runat="server"></div><br />
<div id="scrollImages" runat="server" style="border-style: solid; border-width: 1px; overflow: scroll"></div><br />
<div id="uploadImage">Add Image</div>
<div id="UploadDetails" class="hidden popup">
    <table>
        <tr>
            <td>Description: <input id="UploadDetails_Description" type="text" /></td>
            <td><input id="UploadDetails_File" runat="server" type="file" /></td>
            <td><input id="UploadDetails_Upload" type="button" value="Upload" /></td>
        </tr>
    </table>
</div>