using MVC_Adjacency_list_model.Models;
using System.Net;
using System.Web.Http;

namespace MVC_Adjacency_list_model.Controllers.Api
{
    public class CategoryController : System.Web.Http.ApiController
    {
        CategoryAccessLayer objCategory = new CategoryAccessLayer();

        [HttpDelete]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Administrator)]
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
