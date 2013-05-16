using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bllCharity
{
    public class JsonUser : BaseSettings
    {
        public JsonUser() : base()
        {
            DisplayName = new BaseSettingParameter<string>("DisplayName", string.Empty);
        }

        public BaseSettingParameter<string> DisplayName { get; set; }

        public override string ToString()
        {
            string data = DisplayName.ToString();
            return base.ToString(data);
        }
    }
}
