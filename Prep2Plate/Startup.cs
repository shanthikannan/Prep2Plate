using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Prep2Plate.Startup))]
namespace Prep2Plate
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
