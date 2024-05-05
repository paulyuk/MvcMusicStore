using Microsoft.Owin;
using Owin;
using NSwag.AspNet.Owin;
using System.Web.Http;

[assembly: OwinStartupAttribute(typeof(MvcMusicStore.Startup))]
namespace MvcMusicStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            ConfigureApp(app);

            ConfigureSwagger(app);
        }
    }
}
