using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Adjacency_list_model.Models
{
    //this class will be used to generate Category table
    public class Category
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(30)]
        [RegularExpression(@"^[a-zA-Z0-9]+$+\d", ErrorMessage = "Special character and numbers should not be entered.")]
        public string Name { get; set; }

        [Required]
        public int lft { get; set; }

        [Required]
        public int rgt { get; set; }

        public override string ToString()
        {
            return ("ID= " + ID + ", Name= " +Name + ", lft= "+lft + ", rgt= "+ rgt);
        }
    }
}