    using Microsoft.Ajax.Utilities;
using MVC_Adjacency_list_model.ViewModels;
using MVC_Adjacency_list_model.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
//using System.Web.Http;
using System.Web.Mvc;

namespace MVC_Adjacency_list_model.Controllers
{
    public class CategoryController : Controller
    {
        //throw new HttpResponseException(HttpStatusCode.BadRequest);

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

            //Debug.WriteLine("********************NEW************");

            //foreach(var item in nestedList)///pierwszy poziom
            //{
            //    Debug.WriteLine("FIRST: "+ item.Name); //wyswietlenie pierwszego poziomu


            //    if(item.deeperList != null)//jeśli jest drugi poziom
            //    {
            //        foreach (var deeperItem in item.deeperList) //petla drugiego poziomu
            //        {
            //            Debug.WriteLine("SECOND: " + deeperItem.Name); //wyświetlenie drugiego poziomu

            //            if (deeperItem.deeperList!=null)// jeśli jest trzeci poziom
            //                foreach (var evenDeeperItem in deeperItem.deeperList)//petla trzeciego poziomu
            //                {
            //                  Debug.WriteLine("THIRD: " + evenDeeperItem.Name);//wyświetlenie trzeciego poziomu
            //                }
            //        }
            //    }
            //}
            return View(nestedList);
        }
        

        //GET   /category/Edit/ID
        [HttpGet]
        public ActionResult Rename(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();//ERROR
            }

            //gets data of one node to display it to user
            Category category = objCategory.GetCategoryData(id);

            if (category.ID==0 || category.Name=="ROOT")
            {
                return HttpNotFound();//ERROR
            }

            return View(category);
        }



        //POST   /category/edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rename(Category category)
        {
            //jeśli przesłane id nie jest równe ID zmienianemu obiektowi
   
            
            //if parameter object is not valid
            if (!ModelState.IsValid)
            { 
                return View(category);        
            }

            objCategory.Rename(category);
            return RedirectToAction("Index");
        }



        








    }
}