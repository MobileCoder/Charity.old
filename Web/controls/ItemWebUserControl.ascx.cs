using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AwsWebApp1
{
    public partial class ItemWebUserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TitleControl.Text = Title;
                DescriptionControl.Text = Description;
            }
        }

        public string Title { get; set; }
        public string Description { get; set; }
    }
}