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
                BidAmount.Attributes.Add("ItemId", Id.ToString());
                BidButton.Attributes.Add("ItemId", Id.ToString());
                CurrentBidSpan.Attributes.Add("ItemId", Id.ToString());

                TitleControl.Text = Title;
                TitleControl.NavigateUrl = "~/itemDetails.aspx?Id=" + Id;
                DescriptionControl.Text = Description;
                CurrentBidSpan.InnerText = CurrentBid.ToString("c");

                if (Images != null)
                {
                    for (int i = 0; i < Images.Count; i++)
                    {
                        System.Web.UI.HtmlControls.HtmlImage control = new System.Web.UI.HtmlControls.HtmlImage();
                        control.Src = System.IO.Path.Combine(@"~/images/cache", Images[i].Filename);
                        images.Controls.Add(control);
                    }
                }
            }
        }

        public void SetItem(Item item)
        {
            Id = item.Id;
            Title = item.Title;
            Description = item.Description;
            CurrentBid = item.CurrentBid;
            Images = item.Images;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal CurrentBid { get; set; }
        public ItemImages Images { get; set; }
    }
}