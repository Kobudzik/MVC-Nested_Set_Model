using MVC_Adjacency_list_model.ViewModels;
using MVC_Adjacency_list_model.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;

namespace MVC_Adjacency_list_model.Controllers
{
    public class CategoryController : Controller
    {

        private CategoryRepository objCategory = new CategoryRepository();
        
        //GET  /category
        //GET  /category/index
        public ActionResult Index()
        {            
            List<Category> nestedList = new List<Category>();
            objCategory.GetRootCords(out int lft, out int rgt);
            objCategory.GetChildren(lft, rgt, nestedList);

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
            Category category = objCategory.GetSingle(id);

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
            objCategory.Rename(category);
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
            Category categoryInDB = objCategory.GetSingle(id);

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
            objCategory.InsertInside(category.ID, category.Name);
            return RedirectToAction("Index");
        }



        //GET /category/Move
        [HttpGet]
        [Authorize(Roles = RoleName.Administrator)]
        public ActionResult MoveCategory()
        {
            //gets data of one category to display it to user
            MoveCategoryViewModel moveNodeViewModel = new MoveCategoryViewModel
            {
                allNameList = objCategory.GetAll()
            };

            return View(moveNodeViewModel);
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
                viewModel.allNameList = objCategory.GetAll();
                return View("MoveCategory", viewModel);
            }
            objCategory.Move(viewModel.MovingNodeId, viewModel.NewParentID);
            return RedirectToAction("Index", viewModel);
        }
    }
}