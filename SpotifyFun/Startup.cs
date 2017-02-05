using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SpotifyFun.Startup))]
namespace SpotifyFun
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
