using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Project_4.Startup))]
namespace Project_4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
