using MVC_Adjacency_list_model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace MVC_Adjacency_list_model.App_Start
{
    public static class IocConfigurator
    {
        public static void ConfigureIocCategoryContainer()
        {
            IUnityContainer container = new UnityContainer();
            registerServices(container);
            DependencyResolver.SetResolver(new CategoryDependencyResolver(container));
        }

        public static void registerServices(IUnityContainer container)
        {
            container.RegisterType<ICategoryRepository, CategoryRepository>();
        }
    }
}