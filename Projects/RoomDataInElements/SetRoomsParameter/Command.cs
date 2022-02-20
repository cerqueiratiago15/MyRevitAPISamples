using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB.Architecture;

//Author: Iago Pinto de Cerqueira
namespace SetRoomsParameter
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;

            FilteredElementCollector filteredElement = new FilteredElementCollector(doc);
            Room room =null;
            room = filteredElement.OfCategory(BuiltInCategory.OST_Rooms).OfType<Room>().ToList().FirstOrDefault();
            if (room!=null)
            {
                RoomForm form = new RoomForm(room);
                form.ShowDialog();
            }

            return Result.Succeeded;
        }
    }
}
