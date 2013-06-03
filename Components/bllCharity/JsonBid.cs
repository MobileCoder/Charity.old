using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bllCharity
{
    [Serializable]
    public class JsonBid : JsonObject
    {
        public JsonBid()
            : base()
        {
            Amount = 0;
        }

        public decimal Amount { get; set; }
    }
}
