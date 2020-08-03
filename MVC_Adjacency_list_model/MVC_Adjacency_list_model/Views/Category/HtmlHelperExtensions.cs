using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Adjacency_list_model.ViewModels;



namespace MVC_Adjacency_list_model.Views.Category
{
    public static class HtmlHelperExtensions
    {


        public static string CategoryTree(this HtmlHelper html, IEnumerable<CategoryCarrierViewModel> carrier)
        {
            string htmlOutput = string.Empty;

            if (carrier.Count() > 0 && carrier!=null )
            {
                htmlOutput += "<ul>";
                foreach (var item in carrier)
                {
                    htmlOutput += "<li>";
                    //htmlOutput += item.
                    //htmlOutput += html.CategoryTree(node);
                  //  htmlOutput += "</li>";
                }
                htmlOutput += "</ul>";
            }

            return htmlOutput;
        }
    }
}