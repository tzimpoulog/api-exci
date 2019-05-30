using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ExciApi.Startup))]

namespace ExciApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
