using bllCharity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace AwsWebApp1
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Database.Configuration = ConfigurationManager.ConnectionStrings["LocalConnectionString"];
            if (Database.Configuration == null)
            {
                Database.Configuration = ConfigurationManager.ConnectionStrings["AmazonConnectionString"];
            }

            Utility.Email.Initialize(
                ConfigurationManager.AppSettings["Email.Host"],
                int.Parse(ConfigurationManager.AppSettings["Email.Port"]),
                ConfigurationManager.AppSettings["Email.Address"],
                ConfigurationManager.AppSettings["Email.DisplayName"],
                ConfigurationManager.AppSettings["Email.Password"]);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}