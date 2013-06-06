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

        public void Populate(CharityUser user)
        {
            Id = user.Id;
            DisplayName = user.DisplayName;
        }

        public string DisplayName { get; set; }
        public bool IsActive { get; set; }
    }
}
