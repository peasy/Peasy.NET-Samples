using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Orders.com.Web.MVC.Startup))]
namespace Orders.com.Web.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
