using MVC_Adjacency_list_model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Adjacency_list_model.Models
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        void GetRootCords(out int rootLft, out int rootRgt);
        IEnumerable<NestedCategoriesViewModel> GetChildren(int lft, int rgt, List<NestedCategoriesViewModel> list);
        bool CheckChildren(int lft, int rgt);
        Category GetSingle(int? id);
        void Rename(Category category);
        void InsertInside(int IDWhere, string NameWhat);
        void Delete(int id);
        void Move(int? categoryID, int? newParentID);

    }
}