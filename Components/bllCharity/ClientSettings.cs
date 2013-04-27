using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bllCharity
{
    public class ClientSettings
    {
        public ClientSettings()
        {
            IsValid = false;
            Message = string.Empty;
            DisplayName = string.Empty;
        }

        public bool IsValid { get; set; }
        public string Message { get; set; }
        public string DisplayName { get; set; }

        public override string ToString()
        {
            return "{" + 
                "\"IsValid\":\"" + IsValid + "\"," +
                "\"Message\":\"" + Message + "\"," + 
                "\"DisplayName\":\"" + DisplayName + "\"" +
                    "}";
        }
    }
}
