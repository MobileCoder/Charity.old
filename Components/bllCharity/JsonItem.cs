using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bllCharity
{
    [Serializable]
    public class JsonItem : BaseSettings
    {
        public JsonItem() : base()
        {
            Title = new BaseSettingParameter<string>("Title", string.Empty);
            Description = new BaseSettingParameter<string>("Description", string.Empty);
        }

        public BaseSettingParameter<string> Title { get; set; }
        public BaseSettingParameter<string> Description { get; set; }

        public override string ToString()
        {
            string data = Title.ToString() + "," + Description.ToString();
            return base.ToString(data);
        }
    }
}
