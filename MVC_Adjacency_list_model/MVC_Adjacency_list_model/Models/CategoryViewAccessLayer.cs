using MVC_Adjacency_list_model.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Adjacency_list_model.Models
{
    public class CategoryViewAccessLayer
    {
        string connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\aspnet-MVC_Adjacency_list_model-20200731104721.mdf;Initial Catalog=aspnet-MVC_Adjacency_list_model-20200731104721;Integrated Security=True";


         /////To View all Categories details    
        public IEnumerable<Category> GetAllCategories()
        {
            List<Category> listCategory = new List<Category>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllCategories", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())  //if there is more rows
                {
                    Category category = new Category();
                    category.ID = Convert.ToInt32(rdr["ID"]);
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


        /////To View all children
        ///returns list of category
        public IEnumerable<Category> GetAllChildren(int parentLeft, int parentRight)
        {
            List<Category> listCategory = new List<Category>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllChildren", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@parentlft", parentLeft);
                cmd.Parameters.AddWithValue("@parentrgt", parentRight);
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())  //if there is more rows- adding new category
                {
                    Category category = new Category();
                    category.ID = Convert.ToInt32(rdr["ID"]);
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



        //Get the details of a particular employee  
        public Category GetCategoryData(int? id)
        {
            Category category = new Category();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Categories WHERE ID= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    category.ID = Convert.ToInt32(rdr["ID"]);
                    category.Name = rdr["Name"].ToString();
                    category.lft = Convert.ToInt32(rdr["lft"]);
                    category.rgt = Convert.ToInt32(rdr["rgt"]);
                }
            }
            return category;    //returnssingle object
        }



        //To Update the records of a particluar category 
        public void Rename(Category category)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spRenameNode", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", category.ID);
                cmd.Parameters.AddWithValue("@Name", category.Name);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }




        //To Add new node inside    
        public void InsertInside(int IDWhere, string NameWhat)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spInsertNodeInside", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDWhere", IDWhere);
                cmd.Parameters.AddWithValue("@NameWhat", NameWhat);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


    }
}