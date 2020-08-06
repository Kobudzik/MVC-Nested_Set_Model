using MVC_Adjacency_list_model.App_Start;
using MVC_Adjacency_list_model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Adjacency_list_model.ViewModels
{
    public class MoveCategoryViewModel
    {
        [Required]
        [MovingParentCheck]
        [MoveInsideCheck]
        public int? MovingNodeID { get; set; }

        [Required]
        [MovingCategoryCheck]
        public int? NewParentID { get; set; }

        public IEnumerable<Category> allCategoriesList;
    }
}