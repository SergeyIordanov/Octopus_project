using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Octopus_project.Startup))]
namespace Octopus_project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
