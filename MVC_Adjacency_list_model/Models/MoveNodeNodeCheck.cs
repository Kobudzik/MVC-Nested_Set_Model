using MVC_Adjacency_list_model.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace MVC_Adjacency_list_model.Models
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class MoveNodeCheck : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var myModel = (MoveNodeViewModel)validationContext.ObjectInstance;

            if (myModel.newParentID == myModel.nodeID)
            {
                return new ValidationResult("You have to select diffrent nodes!");
            }

            if (myModel.nodeID == 1)
            {
                return new ValidationResult("You can't move root!");
            }

            return ValidationResult.Success;
        }
    }
}