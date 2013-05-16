using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bllCharity
{
    [Serializable]
    public class JsonBid : BaseSettings
    {
        public JsonBid()
            : base()
        {
            Amount = new BaseSettingParameter<decimal>("Amount", 0);
        }

        public BaseSettingParameter<decimal> Amount { get; set; }

        public override string ToString()
        {
            string data = Amount.ToString();
            return base.ToString(data);
        }
    }
}
