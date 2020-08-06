using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_nested_set_model.Startup))]
namespace MVC_nested_set_model
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
