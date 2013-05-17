using System;
using System.Configuration;

namespace AwsWebApp1
{
    public class Preferences
    {
        private Preferences()
        {
            ImageLargeHeight = Convert.ToInt32(ConfigurationManager.AppSettings["Image.Large.Height"]);
            ImageLargeWidth = Convert.ToInt32(ConfigurationManager.AppSettings["Image.Large.Width"]);
            ImageSmallHeight = Convert.ToInt32(ConfigurationManager.AppSettings["Image.Small.Height"]);
            ImageSmallWidth = Convert.ToInt32(ConfigurationManager.AppSettings["Image.Small.Width"]);
        }

        private static Preferences instance;

        public static Preferences Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Preferences();
                }
                return instance;
            }
        }

        public int ImageLargeHeight { get; set; }
        public int ImageLargeWidth { get; set; }
        public int ImageSmallHeight { get; set; }
        public int ImageSmallWidth { get; set; }
    }
}