using Microsoft.Owin;

using Owin;

[assembly: OwinStartupAttribute(typeof(SportsBook.Web.Startup))]

namespace SportsBook.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
