using MVC_Adjacency_list_model.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Adjacency_list_model.Models
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class MovingCategoryCheck : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var myModel = (MoveCategoryViewModel)validationContext.ObjectInstance;

            if (myModel.NewParentID == myModel.MovingNodeId)
            {
                return new ValidationResult("You have to select diffrent nodes!");
            }
            return ValidationResult.Success;
        }
    }
}