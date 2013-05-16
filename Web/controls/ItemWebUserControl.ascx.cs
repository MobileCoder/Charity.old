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
                System.Web.UI.HtmlControls.HtmlInputText input = new System.Web.UI.HtmlControls.HtmlInputText();
                input.Attributes.Add("ItemId", Id.ToString());
                DivBidding.Controls.Add(input);

                System.Web.UI.HtmlControls.HtmlInputButton button = new System.Web.UI.HtmlControls.HtmlInputButton();
                button.Attributes.Add("ItemId", Id.ToString());
                DivBidding.Controls.Add(button);

                TitleControl.Text = Title;
                DescriptionControl.Text = Description;

                CurrentBidSpan.Attributes.Add("ItemId", Id.ToString());
                CurrentBidSpan.InnerText = CurrentBid.ToString("c");
            }
        }

        public void SetItem(Item item)
        {
            Id = item.Id;
            Title = item.Title;
            Description = item.Description;
            CurrentBid = item.CurrentBid;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal CurrentBid { get; set; }
    }
}