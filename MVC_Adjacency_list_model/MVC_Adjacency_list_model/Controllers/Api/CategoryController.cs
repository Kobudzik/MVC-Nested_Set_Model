using MVC_Adjacency_list_model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MVC_Adjacency_list_model.Controllers.Api
{
    public class CategoryController : ApiController
    {
        CategoryViewAccessLayer objCategory = new CategoryViewAccessLayer();

        [HttpDelete]
        public void DeleteNode(int id)
        {
            var nodeInDb = objCategory.GetCategoryData(id);

            if (nodeInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            objCategory.Delete(id);
        }
    }
}
