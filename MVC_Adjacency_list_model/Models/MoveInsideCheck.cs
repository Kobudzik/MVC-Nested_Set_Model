using MVC_Adjacency_list_model.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;

namespace MVC_Adjacency_list_model.Models
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class MoveInsideCheck : ValidationAttribute
    {
        //private readonly ICategoryRepository _categoryRepository;

        //public NewCheck(ICategoryRepository myRepo)
        //{
        //    _categoryRepository = myRepo;
        //}

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var myModel = (MoveCategoryViewModel)validationContext.ObjectInstance;

            CategoryRepository myRepo = new CategoryRepository();
            List<Category> everyChildrenList = myRepo.GetAll();

            Category movingNode = everyChildrenList.First(m => m.ID == myModel.MovingNodeID);
            Category newParent = everyChildrenList.First(m => m.ID == myModel.NewParentID);

            Debug.WriteLine("MOOVING: " + movingNode.ToString());
            Debug.WriteLine("new PARENT: " + newParent.ToString());

            int newParentLft = newParent.Lft;
            int newParentRgt = newParent.Rgt;

            int movingNodeLft = movingNode.Lft;
            int movingNodeRgt = movingNode.Rgt;

            Debug.WriteLine("moving: LFT " + movingNodeLft + "RGT: " + movingNodeRgt);

            Debug.WriteLine("new PARENT: LFT " + newParentLft + " RGT: " + newParentRgt);



            if (newParentLft > movingNodeLft && newParentRgt < movingNodeRgt)
            {
                Debug.WriteLine(newParentLft + ">" + movingNodeLft + "  AND   "+ newParentRgt + " <" + movingNodeRgt);
                return new ValidationResult("You can't move category inside itself!");
            }
            return ValidationResult.Success;
        }
    }
}