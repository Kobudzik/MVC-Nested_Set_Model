using Microsoft.Owin.Security.Provider;
using MVC_Adjacency_list_model.ViewModels;
using MVC_Adjacency_list_model.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace MVC_Adjacency_list_model.Models
{
    public class CategoryViewAccessLayer
    {
        string connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\aspnet-MVC_Adjacency_list_model-20200731104721.mdf;Initial Catalog=aspnet-MVC_Adjacency_list_model-20200731104721;Integrated Security=True";


        /////To View all Categories details    NOT USED
        public List<Category> GetAllCategories()
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


        //UPDATES ROOT'S LEFT AND RIGHT CORDS
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
                    while (rdr.Read())
                    {
                        rootLft = Convert.ToInt32(rdr["lft"]);
                        rootRgt = Convert.ToInt32(rdr["rgt"]);
                    }
                con.Close();
            }
        }




        /////TO GET CHILDREN- list of carriers 
        public List<CategoryCarrierViewModel> GetChildren(int lft, int rgt, List<CategoryCarrierViewModel> list)
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

                //if there are children with lft and rgt of parameter
                while (rdr.Read())  
                {
                    //GETS ONE CHILD (CARRIER)  OF SPECIFIED PARAMETERS
                    CategoryCarrierViewModel categoryCarrier = new CategoryCarrierViewModel();
                    categoryCarrier.ID = Convert.ToInt32(rdr["ID"]);
                    categoryCarrier.Name = rdr["Name"].ToString();
                    categoryCarrier.lft = Convert.ToInt32(rdr["lft"]);
                    categoryCarrier.rgt = Convert.ToInt32(rdr["rgt"]);

                    //ADDS ONE CHILD- TO PARAMETER'S LIST OBJECT
                    list.Add(categoryCarrier);

                    //CHANGE LFT AND RGT TO CURRENT NODE
                    lft = categoryCarrier.lft;
                    rgt = categoryCarrier.rgt;


                    //IF THERE ARE CHILDREN DEEPER
                    if (CheckIfHasChildren(lft, rgt))
                    {
                        List<CategoryCarrierViewModel> newDeeperList = new List<CategoryCarrierViewModel>();
                        GetChildren(lft, rgt, newDeeperList);
                        list[rowInsideWhile].deeperList = newDeeperList;
                    }
                    rowInsideWhile++;
                }
                con.Close();
            }
            return list;
        }

        /////TO CHECK IF NODE HAS CHILDREN
        public bool CheckIfHasChildren(int lft, int rgt)
        {
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


        //GET THE DETAILS OF A PARTICULAR CHILDREN  
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
            return category;
        }



        //TO RENAME NODE
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




        //TO ADD NEW NODE (inside another one)
        public void InsertInside(int IDWhere, string NameWhat)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spInsertNodeInside", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDWhere", IDWhere);
                cmd.Parameters.AddWithValue("@NameWhat", NameWhat);
                cmd.Parameters.AddWithValue("@myLeft", 0);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


        //TO DELETE NODE
        public void Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteNodeById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@myLeft",0);
                cmd.Parameters.AddWithValue("@myRight",0);
                cmd.Parameters.AddWithValue("@myWidth",0);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }



        public void Move(int? nodeID, int? newParentID )
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                Category node = GetCategoryData(nodeID);
                Debug.WriteLine(node.ToString());
                Category newParent = GetCategoryData(newParentID);
                Debug.WriteLine(newParent.ToString());


                SqlCommand cmd = new SqlCommand("spMoveNode", con);

                    cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@node_id",node.ID );
                        cmd.Parameters.AddWithValue("@node_pos_left", node.lft);
                        cmd.Parameters.AddWithValue("@node_pos_right", node.rgt);

                        cmd.Parameters.AddWithValue("@new_parent_ID", newParent.ID);
                        cmd.Parameters.AddWithValue("@new_parent_pos_right", newParent.rgt);

                        cmd.Parameters.AddWithValue("@node_size", 0);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
            }
        }




    }
}