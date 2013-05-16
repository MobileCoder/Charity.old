using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bllCharity
{
    [Serializable]
    public class BaseSettingParameter<T>
    {
        public BaseSettingParameter()
        {
        }

        public BaseSettingParameter(string key, T value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }
        public T Value { get; set; }

        public override string ToString()
        {
            return "\"" + Key + "\":\"" + Value + "\"";
        }
    }

    [Serializable]
    public class BaseSettings
    {
        public BaseSettings()
        {
            Version = new BaseSettingParameter<string>("Version", "1.0");
            IsValid = new BaseSettingParameter<bool>("IsValid", false);
            Message = new BaseSettingParameter<string>("Message", string.Empty);
            Id = new BaseSettingParameter<int>("Id", 0);
        }

        public BaseSettingParameter<string> Version { get; set; }
        public BaseSettingParameter<bool> IsValid { get; set; }
        public BaseSettingParameter<string> Message { get; set; }
        public BaseSettingParameter<int> Id { get; set; }

        protected string ToString(string data)
        {
            string text = "{" +
                Version.ToString() + "," +
                IsValid.ToString() + "," +
                Message.ToString() + "," +
                Id.ToString();

            if (!string.IsNullOrEmpty(data))
            {
                text += "," + data;
            }

            return (text + "}");
        }
    }
}
