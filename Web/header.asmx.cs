using bllCharity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace AwsWebApp1
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class header : System.Web.Services.WebService
    {
        private CharityUser GetUser(string email, ClientSettings settings)
        {
            CharityUser user = CharityUser.Select(email);
            if (user == null)
            {
                settings.Message = "User not found";
            }
            return user;
        }

        private bool ValidateUser(CharityUser user, string password, ClientSettings settings)
        {
            settings.IsValid = user.ValidatePassword(password);
            if (!settings.IsValid)
            {
                settings.Message = "Invalid password";
            }
            return settings.IsValid;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ValidateUser(string email, string password)
        {
            ClientSettings settings = new ClientSettings();
            CharityUser user = GetUser(email, settings);
            if (user != null)
            {
                if (ValidateUser(user, password, settings))
                {
                    settings.DisplayName = user.DisplayName;
                }
            }
            return settings.ToString();
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ForgotPassword(string email)
        {
            ClientSettings settings = new ClientSettings();
            CharityUser user = GetUser(email, settings);
            if (user != null)
            {
                settings.IsValid = user.ResetPassword();                
                settings.Message = "Your email address will be notified of your reset password";
            }
            return settings.ToString();
        }
    }
}
