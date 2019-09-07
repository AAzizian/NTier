using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using NTier.Mvc.App_Start;
using Owin;
using System.Web.Http;
using static NTier.Mvc.MvcApplication;

[assembly: OwinStartupAttribute(typeof(NTier.Mvc.Startup))]
namespace NTier.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);


            //map custom user provider for signalr
            GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => new CustomUserIdProvider());
            app.MapSignalR();
        }
    }
}
