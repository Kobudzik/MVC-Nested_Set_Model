﻿using MVC_nested_set_model.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace MVC_nested_set_model.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\aspnet-MVC_nested_set_model-20200731104721.mdf;Initial Catalog=aspnet-MVC_nested_set_model-20200731104721;Integrated Security=True";

        /// <summary>
        /// To View all Categories details
        /// </summary>
        /// <returns>List of Category objects</returns>
        public List<Category> GetAll()
        {
            List<Category> listCategoryAll = new List<Category>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllCategories", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())  //if there is more rows
                {
                    Category newCategory = new Category();
                    newCategory.ID = Convert.ToInt32(rdr["ID"]);
                    newCategory.Name = rdr["Name"].ToString();
                    newCategory.Lft = Convert.ToInt32(rdr["lft"]);
                    newCategory.Rgt = Convert.ToInt32(rdr["rgt"]);
                    listCategoryAll.Add(newCategory);
                }
            }
            return listCategoryAll;
        }

        /// <summary>
        /// REFRESHES ROOT'S LEFT AND RIGHT CORDS
        /// </summary>
        /// <param name="rootLft">out int parameter of root left cord</param>
        /// <param name="rootRgt">out int parameter of root right cordparam>
        public void GetRootCords(out int rootLft, out int rootRgt)
        {
            rootLft = 0;
            rootRgt = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spSelectRoot", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    rootLft = Convert.ToInt32(rdr["lft"]);
                    rootRgt = Convert.ToInt32(rdr["rgt"]);
                }
            }
        }

        /// <summary>
        /// Gets children from parent cords 
        /// </summary>
        /// <param name="lft"></param>
        /// <param name="rgt"></param>
        /// <param name="list"></param>
        /// <returns>Returns list of CategoryCarrierViewModel (category with list field) </returns>
        public IEnumerable<Category> GetChildren(int lft, int rgt, List<Category> list)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllChildren", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@parentLft", lft);
                cmd.Parameters.AddWithValue("@parentRgt", rgt);
                con.Open();

                int rowInsideWhile = 0;
                SqlDataReader rdr = cmd.ExecuteReader();

                //if there are children with lft and rgt of parameter
                while (rdr.Read())  
                {
                    //GETS ONE CHILD (CARRIER)  OF SPECIFIED PARAMETERS
                    Category categoryCarrier = new Category
                    {
                        ID = Convert.ToInt32(rdr["ID"]),
                        Name = rdr["Name"].ToString(),
                        Lft = Convert.ToInt32(rdr["lft"]),
                        Rgt = Convert.ToInt32(rdr["rgt"])
                    };

                    //ADDS ONE CHILD- TO PARAMETER'S LIST OBJECT
                    list.Add(categoryCarrier);

                    //CHANGE LFT AND RGT TO CURRENT CATEGORY
                    lft = categoryCarrier.Lft;
                    rgt = categoryCarrier.Rgt;


                    //IF THERE ARE CHILDREN DEEPER
                    if (CheckChildren(lft, rgt))
                    {
                        List<Category> newDeeperList = new List<Category>();
                        GetChildren(lft, rgt, newDeeperList);
                        list[rowInsideWhile].deeperList = newDeeperList;
                    }
                    rowInsideWhile++;
                }
                con.Close();
            }
            return list;
        }

        /// <summary>
        /// /CHECKS IF CATEGORY HAS CHILDREN
        /// </summary>
        /// <param name="lft">lft cord of category</param>
        /// <param name="rgt">rgt cord of category</param>
        /// <returns>RETURNS BOOL</returns>
        public bool CheckChildren(int lft, int rgt)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllChildren", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@parentLft", lft);
                cmd.Parameters.AddWithValue("@parentRgt", rgt);
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                return rdr.Read();
            }
        }

        /// <summary>
        /// GET THE DETAILS OF ONE PARTICULAR CATEGORY  
        /// </summary>
        /// <param name="id">id of category</param>
        /// <returns>returns Category object</returns>
        /// 
        public Category GetSingle(int? id)
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
                    category.Lft = Convert.ToInt32(rdr["lft"]);
                    category.Rgt = Convert.ToInt32(rdr["rgt"]);
                }
            }
            return category;
        }

        /// <summary>
        /// renamse category
        /// </summary>
        /// <param name="category">Category object to rename</param>
        public void Rename(Category category)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spRenameCategory", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ID", category.ID);
                cmd.Parameters.AddWithValue("@Name", category.Name);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Adds new category (inside specified one by id)
        /// </summary>
        /// <param name="IDWhere">Which category will be parent</param>
        /// <param name="NameWhat">How the new category will be named</param>
        public void InsertInside(int IDWhere, string NameWhat)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spInsertCategoryInside", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@IDWhere", IDWhere);
                cmd.Parameters.AddWithValue("@NameWhat", NameWhat);
                cmd.Parameters.AddWithValue("@myLeft", 0);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Deletes a category by ID
        /// </summary>
        /// <param name="id">ID of category to delete</param>
        public void Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteCategoryById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@myLeft",0);
                cmd.Parameters.AddWithValue("@myRight",0);
                cmd.Parameters.AddWithValue("@myWidth",0);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Moves a category to new parent
        /// </summary>
        /// <param name="categoryID">ID of category to move</param>
        /// <param name="newParentID">ID of new parent</param>
        public void Move(int? categoryID, int? newParentID )
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                Category movingCategory = GetSingle(categoryID);
                Category newParent = GetSingle(newParentID);

                SqlCommand cmd = new SqlCommand("spMoveCategory", con);

                cmd.CommandType = CommandType.StoredProcedure;

                //moving category
                cmd.Parameters.AddWithValue("@node_id", movingCategory.ID);
                cmd.Parameters.AddWithValue("@node_pos_left", movingCategory.Lft);
                cmd.Parameters.AddWithValue("@node_pos_right", movingCategory.Rgt);
                //new parent
                cmd.Parameters.AddWithValue("@new_parent_ID", newParent.ID);
                cmd.Parameters.AddWithValue("@new_parent_pos_right", newParent.Rgt);
                
                cmd.Parameters.AddWithValue("@node_size", 0);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Deletes every category (except root)
        /// </summary>
        public void DeleteAllCategories()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteAllCategories", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}