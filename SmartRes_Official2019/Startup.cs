using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmartRes_Official2019.Startup))]
namespace SmartRes_Official2019
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ///app.m
            ConfigureAuth(app);
        }
    }
}
