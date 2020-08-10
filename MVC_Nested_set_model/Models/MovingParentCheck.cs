using MVC_nested_set_model.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace MVC_nested_set_model.Models
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class MovingParentCheck : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var myModel = (MoveCategoryViewModel)validationContext.ObjectInstance;

            if (myModel.NewParentID == myModel.MovingNodeID)
            {
                return new ValidationResult("You have to select diffrent nodes!");
            }

            if (myModel.MovingNodeID == 1)
            {
                return new ValidationResult("You can't move root!");
            }
            return ValidationResult.Success;
        }
    }
}