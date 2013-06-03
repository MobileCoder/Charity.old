using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bllCharity
{
    [Serializable]
    public class JsonItem : JsonObject
    {
        public JsonItem() : base()
        {
            Title = string.Empty;
            Description = string.Empty;
        }

        public string Title { get; set; }
        public string Description { get; set; }
    }
}
