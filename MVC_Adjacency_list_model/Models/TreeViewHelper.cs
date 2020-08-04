using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Adjacency_list_model.Models
{
    public class TreeViewHelper
    {
        /// Create an HTML tree from a recursive collection of items  
        public static TreeView<T> TreeView<T>(this HtmlHelper html, IEnumerable<T> items)
        {
            return new TreeView<T>(html, items);
        }
    }

}
}