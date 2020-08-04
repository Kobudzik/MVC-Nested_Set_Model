using MVC_Adjacency_list_model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Adjacency_list_model.ViewModels
{
    public class MoveNodeViewModel
    {
        public int nodeID { get; set; }
        public int newParentID { get; set; }
        public IEnumerable<Category> allNameList;
    }
}