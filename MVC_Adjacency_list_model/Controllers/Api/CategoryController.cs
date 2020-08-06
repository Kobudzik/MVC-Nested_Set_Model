using MVC_Adjacency_list_model.Models;
using System.Net;
using System.Web.Http;

namespace MVC_Adjacency_list_model.Controllers.Api
{
    public class CategoryController : System.Web.Http.ApiController
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository myRepo)
        {
            _categoryRepository = myRepo;
        }

        [HttpDelete]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Administrator)]
        public void DeleteCategory(int id)
        {
            Category categoryInDb = _categoryRepository.GetSingle(id);

            if (categoryInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            _categoryRepository.Delete(id);
        }

        [HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Administrator)]
        public void DeleteAllCategries()
        {
            _categoryRepository.DeleteAllCategories();
        }
    }
}
