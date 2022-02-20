using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Autodesk
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
#endregion


namespace MyTemplate
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication app = commandData.Application;
            UIDocument uiDoc = app.ActiveUIDocument;
            Document doc = uiDoc.Document;

            MainViewModel vm = new MainViewModel("Hello World!");
            MainWindow wn = new MainWindow(vm);
            wn.ShowDialog();

            return Result.Succeeded;
        }
    }
}
