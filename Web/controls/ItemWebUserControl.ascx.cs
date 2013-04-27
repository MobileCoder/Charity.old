using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using bllCharity;

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
                AmountLabel.Text = Amount.ToString("c");
            }
        }

        public void SetItem(Item item)
        {
            Title = item.Title;
            Description = item.Description;
            Amount = item.Amount;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}