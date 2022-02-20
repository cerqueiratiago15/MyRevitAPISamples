using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB.Architecture;

namespace ElementsInRoom
{
    class RoomAndBoundingBoxElementsClass
    {
        public Room GetRoom { get; set; }
        public List<Element> GetElements { get; set; }
    }
}
