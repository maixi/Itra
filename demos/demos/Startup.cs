using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(demos.Startup))]
namespace demos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
