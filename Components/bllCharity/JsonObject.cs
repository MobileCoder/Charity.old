using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace bllCharity
{
    [Serializable]
    public class JsonObject : ErrorManager
    {
        public JsonObject()
        {
            Version = "1.0";
            IsValid = false;
            Message = string.Empty;
            Id = 0;
        }

        public string Version { get; set; }
        public bool IsValid { get; set; }
        public string Message { get; set; }
        public int Id { get; set; }

        public override string ToString()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(this);
        }
    }
}
