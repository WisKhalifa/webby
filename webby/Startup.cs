using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(webby.Startup))]
namespace webby
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
