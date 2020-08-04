using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_Adjacency_list_model.Startup))]
namespace MVC_Adjacency_list_model
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
