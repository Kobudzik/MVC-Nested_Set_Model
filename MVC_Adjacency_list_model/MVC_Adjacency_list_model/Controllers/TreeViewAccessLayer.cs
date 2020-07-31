using MVC_Adjacency_list_model.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Adjacency_list_model.Controllers
{
    public class TreeViewAccessLayer : Controller
    {
        private ApplicationDbContext _context;

        public TreeViewAccessLayer()
        {
            _context = new ApplicationDbContext();
        }
        
        string connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\aspnet-MVC_Adjacency_list_model-20200731104721.mdf;Initial Catalog=aspnet-MVC_Adjacency_list_model-20200731104721;Integrated Security=True";




        //To View all employees details    
        public IEnumerable<Category> GetAllCategories()
        {
            List<Category> listCategory = new List<Category>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllCategories", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Category category = new Category();
                    category.ID = Convert.ToInt32(rdr["EmployeeID"]);
                    category.Name = rdr["Name"].ToString();
                    category.lft = Convert.ToInt32(rdr["lft"]);
                    category.rgt = Convert.ToInt32(rdr["rgt"]);

                    listCategory.Add(category);
                }
                con.Close();
            }
            Debug.WriteLine(listCategory);
            return listCategory;
        }






        // GET: TreeView
        public ActionResult Index()
        {
            var query = _context.Categories.Where(c => c.rgt == 0); //get parent not and popute
            Debug.WriteLine(query);
            var query2 = _context.Categories.Where(c => c.rgt == 0).ToList(); //get parent not and popute
            Debug.WriteLine(query2);
            return View(query);
        }
    }
}