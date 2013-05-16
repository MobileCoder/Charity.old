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
    public class wsUsers : System.Web.Services.WebService
    {
        private CharityUser GetUser(string email, JsonUser settings)
        {
            CharityUser user = CharityUser.Select(email);
            if (user == null)
            {
                settings.Message.Value = "User not found";
            }
            return user;
        }

        private bool ValidateUser(CharityUser user, string password, JsonUser settings)
        {
            settings.IsValid.Value = user.ValidatePassword(password);
            if (!settings.IsValid.Value)
            {
                settings.Message.Value = "Invalid password";
            }
            return settings.IsValid.Value;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ValidateUser(string email, string password)
        {
            JsonUser settings = new JsonUser();
            CharityUser user = GetUser(email, settings);
            if (user != null)
            {
                if (ValidateUser(user, password, settings))
                {
                    settings.Id.Value = user.Id;
                    settings.DisplayName.Value = user.DisplayName;
                }
            }
            return settings.ToString();
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ForgotPassword(string email)
        {
            JsonUser settings = new JsonUser();
            CharityUser user = GetUser(email, settings);
            if (user != null)
            {
                settings.IsValid.Value = user.ResetPassword();
                settings.Message.Value = "Your email address will be notified of your reset password";
            }
            return settings.ToString();
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string RegisterUser(string email)
        {
            JsonUser settings = new JsonUser();
            string error = string.Empty;
            CharityUser user = CharityUser.Create(email, out error);
            if (user != null)
            {
                settings.Id.Value = user.Id;
                settings.DisplayName.Value = user.DisplayName;
                settings.IsValid.Value = true;
            }
            else
            {
                settings.Message.Value = error;
            }
            return settings.ToString();
        }

        [WebMethod]
        public bool UpdateUser(int Id, string FirstName, string LastName, string Password)
        {
            CharityUser user = CharityUser.Select(Id);
            if (user != null)
            {
                user.FirstName = FirstName;
                user.LastName = LastName;
                user.SetPassword(Password);
                return user.Update();
            }
            return false;
        }
    }
}
