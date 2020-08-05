using System.Web;
using System.Web.Mvc;

namespace MVC_Adjacency_list_model
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());        }
    }
}
