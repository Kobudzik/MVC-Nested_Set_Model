using MVC_Adjacency_list_model.Models;
using System.Net;
using System.Web.Http;

namespace MVC_Adjacency_list_model.Controllers.Api
{
    public class CategoryController : ApiController
    {
        CategoryViewAccessLayer objCategory = new CategoryViewAccessLayer();

        [HttpDelete]
        public void DeleteNode(int id)
        {
            Category nodeInDb = objCategory.GetCategoryData(id);

            if (nodeInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            objCategory.Delete(id);
        }
    }
}
