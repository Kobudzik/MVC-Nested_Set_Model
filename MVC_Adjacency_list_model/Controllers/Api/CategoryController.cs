using MVC_Adjacency_list_model.Models;
using System.Net;
using System.Web.Http;

namespace MVC_Adjacency_list_model.Controllers.Api
{
    public class CategoryController : System.Web.Http.ApiController
    {
        CategoryRepository objRepository = new CategoryRepository();

        [HttpDelete]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Administrator)]
        public void DeleteCategory(int id)
        {
            Category categoryInDb = objRepository.GetSingle(id);

            if (categoryInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            objRepository.Delete(id);
        }

        [HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Administrator)]
        public void DeleteAllCategries()
        {            
            objRepository.DeleteAllCategories();
        }
    }
}
