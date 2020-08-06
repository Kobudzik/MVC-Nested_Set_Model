using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Unity;

namespace MVC_nested_set_model.App_Start
{
    public class CategoryDependencyResolver : IDependencyResolver
    {
        private IUnityContainer _unityContainer;
        public CategoryDependencyResolver(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _unityContainer.Resolve(serviceType);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _unityContainer.ResolveAll(serviceType);
            }
            catch (Exception)
            {
                return new List<object>();
            }
        }
    }
}