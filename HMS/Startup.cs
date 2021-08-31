using Owin;
using Microsoft.Owin;

[assembly: OwinStartupAttribute(typeof(HMS.Startup))]
namespace HMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
