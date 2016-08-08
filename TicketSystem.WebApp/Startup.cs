using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TicketSystem.WebApp.Startup))]
namespace TicketSystem.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
