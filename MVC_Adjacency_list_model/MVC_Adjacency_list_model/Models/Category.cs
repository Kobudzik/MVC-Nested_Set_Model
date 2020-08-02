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



        //[Required(ErrorMessage = "Please enter customer's name.")]
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public int lft { get; set; }

        [Required]
        public int rgt { get; set; }
    }
}