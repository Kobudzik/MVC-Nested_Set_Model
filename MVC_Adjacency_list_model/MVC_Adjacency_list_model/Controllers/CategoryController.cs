using Microsoft.Ajax.Utilities;
using MVC_Adjacency_list_model.ViewModels;
using MVC_Adjacency_list_model.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MVC_Adjacency_list_model.Controllers
{
    public class CategoryController : Controller
    {

        CategoryViewAccessLayer objCategory = new CategoryViewAccessLayer();


        
        //GET  /category
        //GET  /category/index
        public ActionResult Index()
        {
            int lft = 0;
            int rgt = 0;
            List<CategoryCarrierViewModel> nestedList = new List<CategoryCarrierViewModel>();
            objCategory.GetRootLftRgt(out lft, out rgt);
            objCategory.GetChildren(lft, rgt, nestedList);
            return View(nestedList);
        }
        

        //GET   /category/Edit/ID
        [HttpGet]
        public ActionResult Rename(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            //gets data of one node to display it to user
            Category category = objCategory.GetCategoryData(id);

            if (category.ID==0 || category.Name=="ROOT")
            {
                return HttpNotFound();
            }

            return View(category);
        }



        //POST   /category/edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rename(Category category)
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
        public ActionResult NewNode(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            //gets data of one node to display it to user
            Category categoryInDB = objCategory.GetCategoryData(id);

            //if object doesn't exist
            if (categoryInDB.ID!=id)
            {
                Debug.Write("OBJECT DOESNT EXIST!");
                return HttpNotFound();
            }
            return View(categoryInDB);
        }


        //POST   /category/NewNode
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewNode(Category category)
        {
            //if parameter object is not valid
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            objCategory.InsertInside(category.ID, category.Name);
            return RedirectToAction("Index");
        }





    }
}