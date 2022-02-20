using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Five5DWithFilters
{
    public class FilterClass
    {
        public ElementParameterFilter GetFilterElement { get; set; }
        public List<ElementId> ListOfCategories { get; set; }
        public string FilterName { get; set; }
        public FilterToExport ToExport { get; set; }
    }
}
