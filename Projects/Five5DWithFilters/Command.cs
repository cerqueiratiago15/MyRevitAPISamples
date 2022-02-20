using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Five5DWithFilters
{
    
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;
            List<ParameterFilterElement> filters = new List<ParameterFilterElement>();

            FilteredElementCollector filterOfViews = new FilteredElementCollector(doc);
            ViewFamilyType view3D = filterOfViews.OfClass(typeof(ViewFamilyType)).OfType<ViewFamilyType>().ToList().Where(x => x.ViewFamily== ViewFamily.ThreeDimensional).FirstOrDefault();
            View3D new3d = null;

            using (Transaction t = new Transaction(doc, "Create temporary view"))
            {
                t.Start();
                new3d = View3D.CreateIsometric(doc, view3D.Id);
                new3d.DetailLevel = ViewDetailLevel.Fine;
                new3d.DisplayStyle = DisplayStyle.Shading;
                new3d.PartsVisibility = PartsVisibility.ShowPartsAndOriginal;
                MainUser mainForm = new MainUser(uiDoc, new3d);

                mainForm.ShowDialog();
                mainForm.Close();
                t.RollBack();
            }
            return Result.Succeeded;
        }
    }
}
