using Microsoft.Owin;

using OMX.Web;

[assembly: OwinStartup(typeof(Startup))]

namespace OMX.Web
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}