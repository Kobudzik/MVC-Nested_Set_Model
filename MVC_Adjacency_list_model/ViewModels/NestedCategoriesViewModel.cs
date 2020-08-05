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
        [RegularExpression(@"^[a-żA-Ż]+$", ErrorMessage = "Only letters allowed.")]
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