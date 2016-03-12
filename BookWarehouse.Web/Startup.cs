using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookWarehouse.Web.Startup))]
namespace BookWarehouse.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
