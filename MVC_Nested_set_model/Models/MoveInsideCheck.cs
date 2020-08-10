using MVC_nested_set_model.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;

namespace MVC_nested_set_model.Models
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class MoveInsideCheck : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var myModel = (MoveCategoryViewModel)validationContext.ObjectInstance;

            CategoryRepository myRepository = new CategoryRepository();
            List<Category> everyChildrenList = myRepository.GetAll();

            Category movingNode = everyChildrenList.First(m => m.ID == myModel.MovingNodeID);
            int movingNodeLft = movingNode.Lft;
            int movingNodeRgt = movingNode.Rgt;

            Category newParent = everyChildrenList.First(m => m.ID == myModel.NewParentID);
            int newParentLft = newParent.Lft;
            int newParentRgt = newParent.Rgt;

            if (newParentLft > movingNodeLft && newParentRgt < movingNodeRgt)
            {
                return new ValidationResult("You can't move category inside itself!");
            }
            return ValidationResult.Success;
        }
    }
}