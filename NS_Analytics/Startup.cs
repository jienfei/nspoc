using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NS_Analytics.Startup))]
namespace NS_Analytics
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
