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
        [RegularExpression(@"^[a-żA-Ż]+$", ErrorMessage = "Only letters allowed.")]
        public string Name { get; set; }

        [Required]
        public int Lft { get; set; }

        [Required]
        public int Rgt { get; set; }

        public override string ToString()
        {
            return ("ID= " + ID + ", Name= " +Name + ", lft= "+Lft + ", rgt= "+ Rgt);
        }

        public List<Category> deeperList;


        public Category()
        {
            deeperList = new List<Category>();
        }

    }
}