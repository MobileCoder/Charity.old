using bllCharity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AwsWebApp1.controls
{
    public partial class ItemImageWebUserControl : System.Web.UI.UserControl
    {
        enum ImageType
        {
            Large,
            Small
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UploadDetails_File.Attributes.Add("ItemId", ItemId.ToString());
                UploadDetails_File.Accept = "image/*";

                largeImage.Visible = false;
                scrollImages.Visible = false;

                if (Images != null && Images.Count > 0)
                {
                    largeImage.Visible = true;
                    largeImage.Controls.Add(BuildImagesControl(Images[0], ImageType.Large));

                    for (int i = 1; i < Images.Count; i++)
                    {
                        scrollImages.Visible = true;
                        scrollImages.Controls.Add(BuildImagesControl(Images[i], ImageType.Small));
                    }
                }
            }
        }

        public int ItemId { get; set; }
        public ItemImages Images { get; set; }

        private System.Web.UI.HtmlControls.HtmlImage BuildImagesControl(ItemImage image, ImageType type)
        {
            System.Web.UI.HtmlControls.HtmlImage control = new System.Web.UI.HtmlControls.HtmlImage();
            control.Src = System.IO.Path.Combine(@"~/images/cache", image.Filename);
            control.Height = (type == ImageType.Large) ? Preferences.Instance.ImageLargeHeight : Preferences.Instance.ImageSmallHeight;
            control.Width = (type == ImageType.Large) ? Preferences.Instance.ImageLargeWidth : Preferences.Instance.ImageSmallWidth;
            return control;
        }
    }
}