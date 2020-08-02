using Microsoft.Owin.Security.Provider;
using MVC_Adjacency_list_model.Dtos;
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
            return listCategory;
        }


        ///updates root's left and right
        public void GetRootLftRgt(out int rootLft, out int rootRgt)
        {
            rootLft = 0;
            rootRgt = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spSelectRoot", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())  //if there is more rows
                {
                    rootLft = Convert.ToInt32(rdr["lft"]);
                    rootRgt = Convert.ToInt32(rdr["rgt"]);
                }
                con.Close();
            }
        }




        /////To get children- list of carriers 
        public List<CategoryCarrier> GetChildren(int lft, int rgt, List<CategoryCarrier> list)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllChildren", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@parentLft", lft);
                cmd.Parameters.AddWithValue("@parentRgt", rgt);
                con.Open();
                int rowInsideWhile = 0;

                SqlDataReader rdr = cmd.ExecuteReader();

                //JEDEN POZIOM
                //if there are children with lft and rgt of parameter
                while (rdr.Read())  
                {
                        Debug.Write("Selected row inside while no.: " + rowInsideWhile + ", ");

                    //gets one child (carrier)  of specified parameters
                    CategoryCarrier categoryCarrier = new CategoryCarrier();
                    categoryCarrier.ID = Convert.ToInt32(rdr["ID"]);
                    categoryCarrier.Name = rdr["Name"].ToString();
                    categoryCarrier.lft = Convert.ToInt32(rdr["lft"]);
                    categoryCarrier.rgt = Convert.ToInt32(rdr["rgt"]);

                        Debug.Write("Actual name: " + categoryCarrier.Name + ", ");
                        Debug.Write("Actual lft: " + categoryCarrier.lft + ", ");
                        Debug.Write("Actual rgt: " + categoryCarrier.rgt + ", ");

                    //adds one child- to parameter's list object
                    list.Add(categoryCarrier);

                    //change lft and rgt to current node
                    lft = categoryCarrier.lft;
                    rgt = categoryCarrier.rgt;
                    Debug.WriteLine("Current updated cords lft: " + lft + "Current updated cords rgt: " + rgt + ", ");



                    //jesli głębiej są dzieci
                    if (CheckIfHasChildren(lft, rgt))
                    {
                        List<CategoryCarrier> newDeeperList = new List<CategoryCarrier>();
                        GetChildren(lft, rgt, newDeeperList);
                        Debug.WriteLine("ROW INSIDE WHILE" + rowInsideWhile);
                        list[rowInsideWhile].deeperList = newDeeperList;
                    }
                    rowInsideWhile++;


                }
                con.Close();
            }
            return list;
        }

















        /////To check if has children
        public bool CheckIfHasChildren(int lft, int rgt)
        {
            //INITIALIZES LIST

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllChildren", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@parentLft", lft);
                cmd.Parameters.AddWithValue("@parentRgt", rgt);
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                return rdr.Read() ?  true :  false;
            }
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