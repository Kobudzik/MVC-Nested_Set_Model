using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Adjacency_list_model.ViewModels
{
    public class NestedCategoriesViewModel
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(30)]
        [RegularExpression(@"^[a-zA-Z0-9]+$+\d", ErrorMessage = "Special character and numbers should not be entered.")]
        public string Name { get; set; }

        [Required]
        public int Lft { get; set; }

        [Required]
        public int Rgt { get; set; }


        public List<NestedCategoriesViewModel> deeperList;

        public NestedCategoriesViewModel()
        {
            deeperList = new List<NestedCategoriesViewModel>();
        }
    }
}