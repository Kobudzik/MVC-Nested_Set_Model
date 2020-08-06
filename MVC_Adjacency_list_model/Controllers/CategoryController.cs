using MVC_Adjacency_list_model.ViewModels;
using MVC_Adjacency_list_model.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;

namespace MVC_Adjacency_list_model.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository myRepo)
        {
            _categoryRepository = myRepo;
        }

        //GET  /category
        //GET  /category/index
        public ActionResult Index()
        {
            List<Category> nestedList = new List<Category>();
            _categoryRepository.GetRootCords(out int lft, out int rgt);
            _categoryRepository.GetChildren(lft, rgt, nestedList);

            if (User.IsInRole("Administrator"))
            {
                return View("Tree", nestedList);
            }

            return View("ReadOnlyTree", nestedList);
        }

        //GET   /category/Edit/ID
        [HttpGet]
        [Authorize(Roles =RoleName.Administrator)]
        public ActionResult RenameCategory(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            //gets data of one category to display it to user
            Category category = _categoryRepository.GetSingle(id);

            if (category.ID==0 || category.Name=="ROOT")
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //POST   /category/edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Administrator)]
        public ActionResult RenameCategory(Category category)
        {
            //if parameter object is not valid
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            _categoryRepository.Rename(category);
            return RedirectToAction("Index");
        }

        //GET   /category/NewNode/ID
        [HttpGet]
        [Authorize(Roles = RoleName.Administrator)]
        public ActionResult NewCategory(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            //gets data of one category to display it to user
            Category categoryInDB = _categoryRepository.GetSingle(id);

            //if object doesn't exist
            if (categoryInDB.ID != id)
            {
                Debug.Write("OBJECT DOESNT EXIST!");
                return HttpNotFound();
            }
            return View(categoryInDB);
        }

        //POST   /category/NewCategory
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Administrator)]
        public ActionResult NewCategory(Category category)
        {
            //if parameter object is not valid
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            _categoryRepository.InsertInside(category.ID, category.Name);
            return RedirectToAction("Index");
        }

        //GET /category/Move
        [HttpGet]
        [Authorize(Roles = RoleName.Administrator)]
        public ActionResult MoveCategory()
        {
            //gets data of one category to display it to user
            MoveCategoryViewModel viewModel = new MoveCategoryViewModel
            {
                allCategoriesList = _categoryRepository.GetAll()
            };

            return View(viewModel);
        }

        //POST   /category/move
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Administrator)]
        public ActionResult MoveCategory(MoveCategoryViewModel viewModel)
        {
            //if parameter object is not valid
            if (!ModelState.IsValid)
            {
                viewModel.allCategoriesList = _categoryRepository.GetAll();
                return View("MoveCategory", viewModel);
            }

            _categoryRepository.Move(viewModel.MovingNodeID, viewModel.NewParentID);
            return RedirectToAction("Index", viewModel);
        }
    }
}