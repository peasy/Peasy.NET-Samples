using System.Configuration;

namespace Orders.com.Web.MVC.Configuration
{
    public class DIConstructorArgConfig : ConfigurationElement
    {
        [ConfigurationProperty("argumentName")]
        public string ArgumentName
        {
            get { return (string)base["argumentName"]; }
            set { base["argumentName"] = value; }
        }

        [ConfigurationProperty("value")]
        public string Value
        {
            get { return (string)base["value"]; }
            set { base["value"] = value; }
        }

        [ConfigurationProperty("type")]
        public string Type
        {
            get { return (string)base["type"]; }
            set { base["type"] = value; }
        }
    }
}
