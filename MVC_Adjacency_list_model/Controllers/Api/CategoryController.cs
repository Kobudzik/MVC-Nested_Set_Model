using MVC_Adjacency_list_model.Models;
using System.Net;
using System.Web.Http;

namespace MVC_Adjacency_list_model.Controllers.Api
{
    public class CategoryController : ApiController
    {
        CategoryAccessLayer objCategory = new CategoryAccessLayer();

        [HttpDelete]
        public void DeleteCategory(int id)
        {
            Category categoryInDb = objCategory.GetCategoryData(id);

            if (categoryInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            objCategory.Delete(id);
        }
    }
}
