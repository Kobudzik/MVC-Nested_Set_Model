using MVC_Adjacency_list_model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Adjacency_list_model.ViewModels
{
    public class MoveNodeViewModel
    {
        [Required]
        [MoveNodeCheck]
        public int? nodeID { get; set; }


        [Required]
        [MoveNodeNewParentCheck]
        public int? newParentID { get; set; }

        public IEnumerable<Category> allNameList;
    }
}