using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Adjacency_list_model.Controllers
{
    public class CategoryController : Controller
    {
        TreeViewAccessLayer objCategory = new TreeViewAccessLayer();

        // GET: Categories
        public ActionResult Index()
        {
            return View();
        }
    }
}