using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bllCharity
{
    [Serializable]
    public class JsonUser : JsonObject
    {
        public JsonUser() : base()
        {
            DisplayName = string.Empty;
        }

        public string DisplayName { get; set; }
    }
}
