using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Week7LabPin.Startup))]
namespace Week7LabPin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
