using MVC_Adjacency_list_model.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;

namespace MVC_Adjacency_list_model.Models
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class NewCheck : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var myModel = (MoveCategoryViewModel)validationContext.ObjectInstance;
            CategoryRepository myRepo = new CategoryRepository();
            List<Category> everyChildrenList = myRepo.GetAll();
            var movingNodeLft = everyChildrenList.FirstOrDefault(m => m.ID == myModel.MovingNodeID).Lft;
            var newParentLft = everyChildrenList.FirstOrDefault(m => m.ID == myModel.NewParentID).Lft;
            if (newParentLft>= movingNodeLft)
            {     
                return new ValidationResult("You can't move category inside itself!");
            }
            return ValidationResult.Success;
        }
    }
}