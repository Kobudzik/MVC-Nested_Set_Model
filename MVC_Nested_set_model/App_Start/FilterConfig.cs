﻿using System.Web;
using System.Web.Mvc;

namespace MVC_nested_set_model
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());  
        }
    }
}
