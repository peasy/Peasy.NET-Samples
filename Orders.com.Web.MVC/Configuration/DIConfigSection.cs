using System.Configuration;

namespace Orders.com.Web.MVC.Configuration
{
    public class DIConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("bindings")]
        public DIConfigList Bindings
        {
            get { return (DIConfigList)base["bindings"]; }
        }
    }
}
