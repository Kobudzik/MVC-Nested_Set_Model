using MVC_Adjacency_list_model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Adjacency_list_model.Controllers
{
    public class CategoryController : Controller
    {
        CategoryViewAccessLayer objCategory = new CategoryViewAccessLayer();

        // GET /category
        // GET /category/index
        public ActionResult Index()
        {
            List<Category> listCategory = new List<Category>();
            listCategory = objCategory.GetAllChildren(1,20).ToList();
            return View(listCategory);
        }
        

        //////////UPDATE
        //GET   /category/Edit/ID
        [HttpGet]
        public ActionResult Rename(int? id)
        {
            if (id == null)
            {
                return View();//ERROR
            }

            //gets data of one node to display it to user
            Category category = objCategory.GetCategoryData(id);

            if (category == null)
            {
                return View();//ERROR
            }

            return View(category);
        }

        //POST   /category/edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rename(int id, [Bind] Category category)
        {
            //jeśli przesłane id nie jest równe zmienianemu obiektowi
            if (id != category.ID)
            {
                return View();//ERROR
            }


            if (ModelState.IsValid)
            {
                objCategory.Rename(category);
                return RedirectToAction("Index");
            }

            return View(category);
        }
    }
}