using bllCharity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AwsWebApp1.shared
{
    public partial class HeaderControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            /*
            Login control = (Login)LoginView1.FindControl("Login1");
            CharityUser user = CharityUser.Select(control.UserName);
            if (user == null)
            {
                control.FailureText = "User not found";
                return;
            }

            if (!user.ValidatePassword(control.Password))
            {
                control.FailureText = "Incorrect Password";
                return;
            }
            */
            e.Authenticated = true;
        }
    }
}