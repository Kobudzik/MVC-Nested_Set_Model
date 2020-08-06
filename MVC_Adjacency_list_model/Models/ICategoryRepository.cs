using MVC_Adjacency_list_model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Adjacency_list_model.Models
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        void GetRootCords(out int rootLft, out int rootRgt);
        IEnumerable<Category> GetChildren(int lft, int rgt, List<Category> list);
        bool CheckChildren(int lft, int rgt);
        Category GetSingle(int? id);
        void Rename(Category category);
        void InsertInside(int IDWhere, string NameWhat);
        void Delete(int id);
        void Move(int? categoryID, int? newParentID);
        void DeleteAllCategories();
    }
}